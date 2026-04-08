using Application.Dtos.Planning;
using Application.Interfaces.Repositories;
using Application.Responses.Planning;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PlanningRepository : BaseRepository<Planning>, IPlanningRepository
    {
        public PlanningRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<bool> ExistsByAvailabilityIdAsync(int availabilityId)
        {
            return await _dbSet.AnyAsync(p => p.AvailabilityId == availabilityId);
        }

        public async Task<PlanningPeriod> GetOrCreatePeriodAsync(int departmentId, int month, int year)
        {
            var period = await PlannerContext.PlanningPeriods
                .FirstOrDefaultAsync(pp => pp.DepartmentId == departmentId
                    && pp.Month == month
                    && pp.Year == year);

            if (period == null)
            {
                period = new PlanningPeriod
                {
                    DepartmentId = departmentId,
                    Month = month,
                    Year = year
                };
                await PlannerContext.PlanningPeriods.AddAsync(period);
                await PlannerContext.SaveChangesAsync();
            }

            return period;
        }

        public async Task<Planning?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Availability)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Planning?> GetByIdWithPeriodAsync(int id)
        {
            return await _dbSet
                .Include(p => p.PlanningPeriod)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<AvailabilityDetailDto?> GetByAvailabilityDetailAsync(int availabilityId)
        {
            return await PlannerContext.Availabilities
                .AsNoTracking()
                .Where(a => a.Id == availabilityId)
                .Select(a => new AvailabilityDetailDto
                {
                    DepartmentId = a.DepartmentMember.DepartmentId,
                    MemberId = a.DepartmentMember.MemberId,
                    Month = a.TabServicePrg.PrgDate.Date!.Value.Month,
                    Year = a.TabServicePrg.PrgDate.Date!.Value.Year,
                    ProgramDate = a.TabServicePrg.PrgDate.Date!.Value,
                    MemberName = a.DepartmentMember.Member.Name + " " + a.DepartmentMember.Member.LastName.Substring(0, 1) + ".",
                    StartTime = a.TabServicePrg.TabServices.StartTime,
                    EndTime = a.TabServicePrg.TabServices.EndTime
                })
                .FirstOrDefaultAsync();
        }

        /// <summary>
        ///     Extension 3g — Détecte les chevauchements horaires cross-département.
        /// </summary>
        public async Task<List<OverlapConflictDto>> GetOverlappingAssignmentsAsync(
            Guid memberId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(p =>
                    p.Availability.DepartmentMember.MemberId == memberId
                    && p.Availability.TabServicePrg.PrgDate.Date.HasValue
                    && p.Availability.TabServicePrg.PrgDate.Date.Value == date
                    && p.Availability.TabServicePrg.TabServices.StartTime < endTime
                    && p.Availability.TabServicePrg.TabServices.EndTime > startTime
                    && !p.PlanningPeriod.IndArchived)
                .Select(p => new OverlapConflictDto
                {
                    DepartmentName = p.Availability.DepartmentMember.Department.Name,
                    ServiceName = p.Availability.TabServicePrg.DisplayName,
                    StartTime = p.Availability.TabServicePrg.TabServices.StartTime.ToString(),
                    EndTime = p.Availability.TabServicePrg.TabServices.EndTime.ToString()
                })
                .ToListAsync();
        }

        public async Task<List<MonthlyPlanningResponse>> GetMonthlyPlanningAsync(int month, int year, int departmentId)
        {
            // Récupérer le PlanningPeriod pour ce département/mois/année
            var periodId = await PlannerContext.PlanningPeriods
                .AsNoTracking()
                .Where(pp => pp.DepartmentId == departmentId && pp.Month == month && pp.Year == year)
                .Select(pp => pp.Id)
                .FirstOrDefaultAsync();

            if (periodId == 0)
            {
                return new List<MonthlyPlanningResponse>();
            }

            // Projection plate — uniquement les champs utilisés par le front
            var rawData = await _dbSet
                .AsNoTracking()
                .Where(p => p.PlanningPeriodId == periodId)
                .Select(p => new
                {
                    ProgramName = p.Availability.TabServicePrg.PrgDate.PrgDepartmentInfo.DepartmentProgram.Program.Name,
                    ProgramShortName = p.Availability.TabServicePrg.PrgDate.PrgDepartmentInfo.DepartmentProgram.Program.ShortName,
                    Date = p.ProgramDate,
                    ServiceName = p.Availability.TabServicePrg.DisplayName,
                    StartTime = p.Availability.TabServicePrg.TabServices.StartTime,
                    PlanningId = p.Id,
                    p.MemberName,
                    PosteName = p.Poste != null ? p.Poste.Name : null,
                    p.IndTraining
                })
                .ToListAsync();

            // Groupement en mémoire — Programme → Date → Service → Membres
            return rawData
                .GroupBy(r => new { r.ProgramName, r.ProgramShortName })
                .Select(prgGroup => new MonthlyPlanningResponse
                {
                    ProgramName = prgGroup.Key.ProgramName,
                    ProgramShortName = prgGroup.Key.ProgramShortName,
                    Dates = prgGroup
                        .GroupBy(r => r.Date)
                        .OrderBy(g => g.Key)
                        .Select(dateGroup => new PlanningDateResponse
                        {
                            Date = dateGroup.Key,
                            Services = dateGroup
                                .GroupBy(r => r.ServiceName)
                                .OrderBy(g => g.Min(x => x.StartTime))
                                .Select(svcGroup => new PlanningServiceResponse
                                {
                                    ServiceName = svcGroup.Key,
                                    Members = svcGroup
                                        .OrderBy(r => r.MemberName)
                                        .Select(r => new PlannedMemberResponse
                                        {
                                            PlanningId = r.PlanningId,
                                            MemberName = r.MemberName,
                                            PosteName = r.PosteName,
                                            IndTraining = r.IndTraining
                                        }).ToList()
                                }).ToList()
                        }).ToList()
                })
                .OrderBy(p => p.ProgramName)
                .ToList();
        }

    }
}

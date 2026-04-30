using Application.Interfaces.Repositories;
using Application.Responses.Planning;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PlanningPeriodRepository : BaseRepository<PlanningPeriod>, IPlanningPeriodRepository
    {
        public PlanningPeriodRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<int> ArchivePastPeriodsAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var periodsToArchive = await _dbSet
                .Where(pp => !pp.IndArchived)
                .ToListAsync();

            var archived = 0;

            foreach (var period in periodsToArchive)
            {
                var lastDayOfMonth = new DateOnly(period.Year, period.Month, DateTime.DaysInMonth(period.Year, period.Month));

                if (lastDayOfMonth < today)
                {
                    period.IndArchived = true;
                    archived++;
                }
            }

            if (archived > 0)
            {
                await PlannerContext.SaveChangesAsync();
            }

            return archived;
        }

        public async Task<PlanningPeriod?> GetByDepartmentMonthYearAsync(int departmentId, int month, int year)
        {
            return await _dbSet
                .FirstOrDefaultAsync(pp => pp.DepartmentId == departmentId
                    && pp.Month == month
                    && pp.Year == year);
        }

        public async Task DeletePublishedPlanningsAsync(int periodId)
        {
            var existing = await PlannerContext.PublishedPlannings
                .Where(pp => pp.PlanningPeriodId == periodId)
                .ToListAsync();

            if (existing.Any())
            {
                PlannerContext.PublishedPlannings.RemoveRange(existing);
                await PlannerContext.SaveChangesAsync();
            }
        }

        public async Task AddPublishedPlanningsAsync(List<PublishedPlanning> plannings)
        {
            if (plannings.Any())
            {
                await PlannerContext.PublishedPlannings.AddRangeAsync(plannings);
                await PlannerContext.SaveChangesAsync();
            }
        }

        public async Task<List<MyPlanningResponse>> GetMyPlanningAsync(Guid memberId, int month, int year, int? departmentId)
        {
            var query = PlannerContext.PublishedPlannings
                .AsNoTracking()
                .Where(pp => pp.MemberId == memberId
                    && pp.ProgramDate.Month == month
                    && pp.ProgramDate.Year == year
                    && pp.PlanningPeriod.IndPublished);

            if (departmentId.HasValue)
            {
                query = query.Where(pp => pp.PlanningPeriod.DepartmentId == departmentId.Value);
            }

            // 1. Récupérer les assignations du membre
            var myPlannings = await query
                .OrderBy(pp => pp.ProgramDate)
                .Select(pp => new
                {
                    pp.ProgramDate,
                    pp.ProgramName,
                    pp.ProgramShortName,
                    pp.ServiceName,
                    pp.PosteName,
                    pp.IndTraining,
                    DepartmentName = pp.PlanningPeriod.Department.Name,
                    DepartmentId = pp.PlanningPeriod.DepartmentId
                })
                .ToListAsync();

            if (myPlannings.Count == 0)
            {
                return [];
            }

            // 2. Récupérer tous les coéquipiers pour les mêmes dates/services/départements (1 seule requête)
            var dates = myPlannings.Select(p => p.ProgramDate).Distinct().ToList();
            var deptIds = myPlannings.Select(p => p.DepartmentId).Distinct().ToList();

            var allTeammates = await PlannerContext.PublishedPlannings
                .AsNoTracking()
                .Where(pp => pp.MemberId != memberId
                    && dates.Contains(pp.ProgramDate)
                    && deptIds.Contains(pp.PlanningPeriod.DepartmentId)
                    && pp.ProgramDate.Month == month
                    && pp.ProgramDate.Year == year
                    && pp.PlanningPeriod.IndPublished)
                .Select(pp => new
                {
                    pp.ProgramDate,
                    pp.ServiceName,
                    pp.ProgramName,
                    pp.MemberName,
                    pp.PosteName,
                    pp.IndTraining,
                    DepartmentId = pp.PlanningPeriod.DepartmentId
                })
                .ToListAsync();

            // 3. Assembler
            return myPlannings.Select(p => new MyPlanningResponse
            {
                Date = p.ProgramDate,
                ProgramName = p.ProgramName,
                ProgramShortName = p.ProgramShortName,
                ServiceName = p.ServiceName,
                PosteName = p.PosteName,
                IndTraining = p.IndTraining,
                DepartmentName = p.DepartmentName,
                Teammates = allTeammates
                    .Where(t => t.ProgramDate == p.ProgramDate
                        && t.ServiceName == p.ServiceName
                        && t.ProgramName == p.ProgramName
                        && t.DepartmentId == p.DepartmentId)
                    .Select(t => new MyPlanningTeammate
                    {
                        MemberName = t.MemberName,
                        PosteName = t.PosteName,
                        IndTraining = t.IndTraining
                    })
                    .OrderBy(t => t.MemberName)
                    .ToList()
            }).ToList();
        }

        public async Task<List<TeamPlanningResponse>> GetTeamPlanningAsync(int departmentId, int month, int year)
        {
            return await PlannerContext.PublishedPlannings
                .AsNoTracking()
                .Where(pp => pp.PlanningPeriod.DepartmentId == departmentId
                    && pp.ProgramDate.Month == month
                    && pp.ProgramDate.Year == year
                    && pp.PlanningPeriod.IndPublished)
                .OrderBy(pp => pp.ProgramDate)
                .ThenBy(pp => pp.ServiceName)
                .ThenBy(pp => pp.MemberName)
                .Select(pp => new TeamPlanningResponse
                {
                    Date = pp.ProgramDate,
                    ProgramName = pp.ProgramName,
                    ProgramShortName = pp.ProgramShortName,
                    ServiceName = pp.ServiceName,
                    MemberName = pp.MemberName,
                    PosteName = pp.PosteName,
                    IndTraining = pp.IndTraining
                })
                .ToListAsync();
        }
    }
}

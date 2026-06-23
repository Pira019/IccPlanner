using Application.Dtos.Program;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DepartmentProgramRepository : BaseRepository<DepartmentProgram>, IDepartmentProgramRepository
    {
        public DepartmentProgramRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<DepartmentProgram?> FindDepartmentProgramAsync(List<int> departmentIds, int programId, bool IndRecurent)
        {
            return await _dbSet.Where(dp => departmentIds.Contains(dp.DepartmentId) &&
                   programId == dp.ProgramId && dp.IndRecurent == IndRecurent
                   && !dp.Program.IsDeleted)
                   .Include(dp => dp.Department)
                   .Include(dp => dp.Program)
                   .FirstOrDefaultAsync();
        }

        public async Task<DepartmentProgram?> FindSoftDeletedAsync(List<int> departmentIds, int programId, bool indRec)
        {
            return await _dbSet
                .IgnoreQueryFilters()
                .Where(dp => departmentIds.Contains(dp.DepartmentId)
                    && dp.ProgramId == programId
                    && dp.IndRecurent == indRec
                    && dp.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(DepartmentProgram entity)
        {
            PlannerContext.DepartmentPrograms.Update(entity);
            await PlannerContext.SaveChangesAsync();
        }

        public async Task SoftDeleteByProgramIdAsync(int programId)
        {
            await _dbSet
                .Where(dp => dp.ProgramId == programId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(dp => dp.IsDeleted, true)
                    .SetProperty(dp => dp.DeletedAt, DateTimeOffset.UtcNow));
        }

        public async Task<DepartmentProgram?> GetSoftDeletedByIdAsync(int id)
        {
            return await _dbSet
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(dp => dp.Id == id && dp.IsDeleted);
        }

        public async Task HardDeleteAsync(int id)
        {
            await _dbSet
                .IgnoreQueryFilters()
                .Where(dp => dp.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<RecurrentProgramDto>> GetRecurrentProgramsForDateGenerationAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(dp => dp.IndRecurent && dp.PrgDepartmentInfos != null)
                .Select(dp => new RecurrentProgramDto
                {
                    Id = dp.Id,
                    DaysAhead = dp.Department.RecurrentDaysAhead,
                    DateEnd = dp.DateF,
                    PrgDepartmentInfos = dp.PrgDepartmentInfos!.Select(pdi => new RecurrentPrgDepartmentInfoDto
                    {
                        Id = pdi.Id,
                        Day = pdi.Day,
                        ExistingDates = pdi.PrgDate
                            .Where(pd => pd.Date.HasValue)
                            .Select(pd => pd.Date!.Value)
                            .ToList(),
                        ServiceTemplates = pdi.PrgDate
                            .SelectMany(pd => pd.TabServicePrgs)
                            .Select(tsp => new ServiceTemplateDto
                            {
                                TabServicesId = tsp.TabServicesId,
                                DisplayName = tsp.DisplayName,
                                ArrivalTimeOfMember = tsp.ArrivalTimeOfMember,
                                Days = tsp.Days,
                                Notes = tsp.Notes
                            })
                            .Distinct()
                            .ToList()
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<int> CreateRecurrentDatesWithServicesAsync(List<PrgDate> newDates, List<ServiceTemplateDto> serviceTemplates)
        {
            await PlannerContext.Set<PrgDate>().AddRangeAsync(newDates);
            await PlannerContext.SaveChangesAsync();

            if (serviceTemplates.Any())
            {
                var newServices = new List<TabServicePrg>();
                foreach (var newDate in newDates)
                {
                    foreach (var template in serviceTemplates)
                    {
                        newServices.Add(new TabServicePrg
                        {
                            PrgDateId = newDate.Id,
                            TabServicesId = template.TabServicesId,
                            DisplayName = template.DisplayName,
                            ArrivalTimeOfMember = template.ArrivalTimeOfMember,
                            Days = template.Days,
                            Notes = template.Notes
                        });
                    }
                }

                await PlannerContext.Set<TabServicePrg>().AddRangeAsync(newServices);
                await PlannerContext.SaveChangesAsync();
            }

            return newDates.Count;
        }
    }
}

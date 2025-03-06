using System.Collections.Immutable;
using System.Linq;
using Application.Dtos.Department;
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

        public async Task<IEnumerable<DepartmentProgramExistingDto>> GetExistingProgramDepartmentsAsync(IEnumerable<DepartmentProgram> departmentPrograms)
        {
            // Extract lists for filtering (improves performance)
            var programIds = departmentPrograms.Select(dp => dp.ProgramId).ToHashSet();
            var departmentIds = departmentPrograms.Select(dp => dp.DepartmentId).ToHashSet();
            var startAtDates = departmentPrograms.Select(dp => dp.StartAt).ToHashSet();

            var result = await _dbSet
                .Where(dp => programIds.Contains(dp.ProgramId) &&
                             departmentIds.Contains(dp.DepartmentId) &&
                             startAtDates.Contains(dp.StartAt)) 
                .Select(dp => new DepartmentProgramExistingDto
                {
                    ProgramId = dp.ProgramId,
                    DepartmentId = dp.DepartmentId,
                    StartAt = dp.StartAt
                })
                .ToListAsync();

            return result;
        }

    }
}

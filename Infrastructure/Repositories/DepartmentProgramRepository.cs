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

        public async Task<DepartmentProgram?> GetFirstExistingDepartmentProgramAsync(List<int> departmentIds, int programId, string programType)
        {
            return await _dbSet.Where(dp => departmentIds.Contains(dp.DepartmentId) &&
                   programId == dp.ProgramId && dp.Type == programType)
                   .Include(dp => dp.Department)
                   .Include(dp => dp.Program)
                   .FirstOrDefaultAsync();
        }
    }
}

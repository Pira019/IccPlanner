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

        public async Task<List<int>> InsertAlDepartmentProgramlAsync(IEnumerable<DepartmentProgram> entities)
        {
            var ids = new List<int>();

            foreach (var chunk in entities.Chunk(1000))
            {
                await _dbSet.AddRangeAsync(chunk);
                await PlannerContext.SaveChangesAsync();

                ids.AddRange(chunk.Select(e => e.Id));
            }
            return ids;
        }

        public async Task<DepartmentProgram?> GetFirstExistingDepartmentProgramAsync(List<int> departmentIds, int programId, bool indRecurent)
        {
            return await _dbSet.Where(dp => departmentIds.Contains(dp.DepartmentId) &&
                   programId == dp.ProgramId && dp.IndRecurent == indRecurent)
                   .Include(dp => dp.Department)
                   .Include(dp => dp.Program)
                   .FirstOrDefaultAsync();
        }
    }
}

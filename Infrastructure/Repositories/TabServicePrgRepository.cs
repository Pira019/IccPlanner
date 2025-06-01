// Ignore Spelling: Prg

using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TabServicePrgRepository : BaseRepository<TabServicePrg>, ITabServicePrgRepository
    {
        public TabServicePrgRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<int?> GetDepartmentIdByServicePrgId(int servicePrgId)
        {
            return await _dbSet
                 .Include(service => service.PrgDate)
                  .ThenInclude(service => service.PrgDepartmentInfo)
                 .Where(service => service.Id == servicePrgId)
                 .Select(service => service.PrgDate.PrgDepartmentInfo.DepartmentProgram.DepartmentId)
                 .FirstOrDefaultAsync();
        }

        public Task<bool> IsServicePrgExist(int tabServiceId, int prgDateId)
        {
            return _dbSet.Where(service => service.TabServicesId == tabServiceId && service.PrgDateId == prgDateId).AnyAsync();
        }
    }
}

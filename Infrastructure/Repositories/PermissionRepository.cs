using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    ///     Permet de gérer les permissions.
    /// </summary>
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<IEnumerable<Permission>> GetByIdsAsync(List<int> ids)
        {
            return await _dbSet.Where(p => ids.Contains(p.Id)).ToListAsync();
        }
    }
}

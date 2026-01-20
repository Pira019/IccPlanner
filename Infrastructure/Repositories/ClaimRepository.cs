using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    ///     
    /// </summary>
    public class ClaimRepository : BaseRepository<IdentityUserClaim<string>>, IClaimRepository
    {
        public ClaimRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }


        /// <summary>
        ///     
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string?>> GetClaimsValuesByUserIdAsync(string userId)
        {
           return await _dbSet
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.ClaimValue)
                .ToListAsync();
        }

        public async Task<bool> HasClaimAsync(string userId, string permissionName)
        {
            return await _dbSet.AnyAsync(uc =>
                                        uc.UserId == userId &&
                                        uc.ClaimValue != null &&
                                         EF.Functions.ILike(uc.ClaimValue, permissionName)
                                    );
        }
    }
}

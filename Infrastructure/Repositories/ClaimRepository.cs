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
    public class ClaimRepository : IClaimRepository
    {
        private readonly IccPlannerContext _iccPlannerContext;

        public ClaimRepository(IccPlannerContext iccPlannerContext)
        {
            _iccPlannerContext = iccPlannerContext;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string?>> GetClaimsValuesByUserIdAsync(string userId)
        {
           return await _iccPlannerContext.Set<IdentityUserClaim<string>>()
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.ClaimValue)
                .ToListAsync();
        }
    }
}

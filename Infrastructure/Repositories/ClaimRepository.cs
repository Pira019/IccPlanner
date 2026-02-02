using System.Security.Claims;
using Application.Interfaces.Repositories;
using Application.Responses.Account;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    ///     
    /// </summary>
    public class ClaimRepository : BaseRepository<IdentityUserClaim<string>>, IClaimRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimRepository(IccPlannerContext plannerContext,IHttpContextAccessor httpContextAccessor) : base(plannerContext)
        {
            _httpContextAccessor = httpContextAccessor;
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

        public ClaimsResponse GetUserClaims()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            return new ClaimsResponse
            {
                Roles = user.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList(),

                Permissions = user.Claims
                    .Where(c => c.Type == ClaimType.Permission.ToString())
                    .Select(c => c.Value)
                    .ToList(),
            };
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

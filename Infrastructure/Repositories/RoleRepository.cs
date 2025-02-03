using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Cete classe permet de faire les actions dans la table Role
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IccPlannerContext _iccPlannerContext;

        public RoleRepository(RoleManager<Role> roleManager, IccPlannerContext context)
        {
            _roleManager = roleManager;
            _iccPlannerContext = context;
        }
         

        public Task<IdentityResult> CreateAsync(Role role)
        {
            return CreateAsync(role);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _iccPlannerContext.Roles.ToListAsync();
        }
    }
}

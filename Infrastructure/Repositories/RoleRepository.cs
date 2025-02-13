using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Cette classe permet de faire les actions dans la table Role
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IccPlannerContext _iccPlannerContext;

        public RoleRepository(RoleManager<Role> roleManager, IccPlannerContext iccPlannerContext)
        {
            _roleManager = roleManager;
            _iccPlannerContext = iccPlannerContext;
        }

        public Task<IdentityResult> CreateAsync(Role role)
        {
            return _roleManager.CreateAsync(role);
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _iccPlannerContext.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }
    }
}

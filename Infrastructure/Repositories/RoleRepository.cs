using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Cete classe permet de faire les actions dans la table Role
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleRepository(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        
        public Task<IdentityResult> CreateAsync(Role role)
        {
            return _roleManager.CreateAsync(role);
        }

    }
}

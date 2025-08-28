using Application.Dtos.Role;
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

        /// <summary>
        ///     Récupérer tout les roles.
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetRolesDto>> GetAllRoles()
        {
            return await _roleManager.Roles 
                .Select
                (roles => new GetRolesDto
                {
                    Id = roles.Id,
                    Description = roles.Description,
                    Name = roles.Name ?? string.Empty,
                    NbrUsers = _iccPlannerContext.UserRoles.Count( role => roles.Id == role.RoleId),
                    Permissions = roles.Permissions
                    .Select ( permisions => new PermissionDto 
                    {
                        Id = permisions.Id,
                        Name = permisions.Name,
                        Description = permisions.Description

                    }).ToList()
                }).ToListAsync();
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }
    }
}

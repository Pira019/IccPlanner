using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Role;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    /// <summary>
    /// Permet de gerer les actions d'un Role
    /// </summary>
    public class RoleService: IRoleService
    {
         private readonly  IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<IdentityResult> CreateRole(CreateRoleRequest createRoleRequest)
        {
            var role = new Role { Name = createRoleRequest.Name, Description = createRoleRequest.Description };
            return _roleRepository.CreateAsync(role);    
        }
    }
}

using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Role;
using Domain.Entities;

namespace Application.Services
{
    /// <summary>
    /// Permet de gerer les actions d'un Role
    /// </summary>
    public class RoleService: IRoleService
    {
         private readonly  IRoleRepository<Role> _roleRepository;

        public RoleService(IRoleRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task CreateRole(CreateRoleRequest createRoleRequest)
        {
            throw new NotImplementedException();
        }
    }
}

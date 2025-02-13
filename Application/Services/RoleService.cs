using System.Data;
using Application.Dtos.Role;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Role;
using Application.Responses.Role;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    /// <summary>
    /// Permet de gérer les actions d'un Role
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public Task<IdentityResult> CreateRole(CreateRoleRequest createRoleRequest)
        {
            var role = new Role { Name = createRoleRequest.Name, Description = createRoleRequest.Description };
            return _roleRepository.CreateAsync(role);
        }

        public async Task<ICollection<GetRolesDto>> GetAll()
        {
            var roles = await _roleRepository.GetAllRoles();

            return _mapper.Map<ICollection<GetRolesDto>>(roles);
        }

        public async Task<CreateRoleResponse> GetRoleByName(string roleName)
        {
            var role = await _roleRepository.GetRoleByNameAsync(roleName);
            return _mapper.Map<CreateRoleResponse>(role);
        }
    }
}

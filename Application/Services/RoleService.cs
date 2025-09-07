using System.Data;
using Application.Dtos.Role;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Role;
using Application.Responses.Role;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    /// <summary>
    /// Permet de gérer les actions d'un Role
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _IPermissionRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper, IPermissionRepository permissionRepository)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _IPermissionRepository = permissionRepository;
        }

        public async Task<IdentityResult> CreateRole([FromBody] CreateRoleRequest createRoleRequest)
        {
            var role = new Role { Name = createRoleRequest.Name, Description = createRoleRequest.Description };  
            role.Permissions.AddRange(await _IPermissionRepository.GetByIdsAsync(createRoleRequest.PermissionIds) ); 
            return await _roleRepository.CreateAsync(role);
        }
        public async Task<CreateRoleResponse> GetRoleByName(string roleName)
        {
            var role = await _roleRepository.GetRoleByNameAsync(roleName);
            return _mapper.Map<CreateRoleResponse>(role);
        }
    }
}

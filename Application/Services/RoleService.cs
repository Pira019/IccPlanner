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
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        /// <summary>
        ///     Définition des rôles par défaut avec leurs permissions associées.
        /// </summary>
        private static readonly List<(string Name, string Description, bool IndSys, string[] Permissions)> DefaultRoles =
        [
            ("Admin", "Administrateur", true,
            [
                "CanReadRole", "CanCreateRole", "CanCreateMinistry",
                "CanManagDepart", "depart:manager", "manage_program_details",
                "prg:manager", "ManagerService"
            ]),
            ("Ap", "Assistant Pasteur", true,
            [
                "CanCreateMinistry", "CanAttributDepartmentChef", "CanManagDepart",
                "manage_program_details", "prg:manager"
            ]),
            ("Pasteur", "Pasteur", true,
            [
                "CanCreateMinistry", "CanManagDepart", "CanCreateDepartement",
                "manage_program_details", "prg:manager"
            ]),
            ("Berger", "Berger", true,
            [
                "CanCreateMinistry", "CanManagDepart",
                "manage_program_details", "prg:manager"
            ]),
        ];

        public RoleService(IRoleRepository roleRepository, IMapper mapper, IPermissionRepository permissionRepository, IAccountRepository accountRepository)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _IPermissionRepository = permissionRepository;
            _accountRepository = accountRepository;
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

        /// <inheritdoc />
        public async Task SeedDefaultRolesAsync()
        {
            // Charger toutes les permissions en une seule requête
            var allPermissions = (await _IPermissionRepository.GetAllAsync()).ToList();

            foreach (var (name, description, indSys, permissionNames) in DefaultRoles)
            {
                var existingRole = await _roleRepository.GetRoleWithPermissionsAsync(name);

                if (existingRole == null)
                {
                    // Créer le rôle
                    var newRole = new Role { Name = name, Description = description, IndSys = indSys };
                    var permissions = allPermissions.Where(p => permissionNames.Contains(p.Name)).ToList();
                    newRole.Permissions.AddRange(permissions);
                    await _roleRepository.CreateAsync(newRole);
                }
                else
                {
                    // Mettre à jour les permissions si le rôle existe déjà
                    var permissions = allPermissions.Where(p => permissionNames.Contains(p.Name)).ToList();
                    if (existingRole.Permissions.Count != permissions.Count ||
                        !existingRole.Permissions.All(ep => permissions.Any(p => p.Id == ep.Id)))
                    {
                        await _roleRepository.UpdateRolePermissionsAsync(existingRole, permissions);
                    }
                }
            }
        }

        /// <inheritdoc />
        public async Task<Result<bool>> AssignRoleAsync(string userId, string roleName)
        {
            var user = await _accountRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return Result<bool>.Fail("Utilisateur introuvable.");
            }

            // Assigner le rôle
            await _accountRepository.AddUserRole(user, roleName);

            // Récupérer les permissions du rôle
            var role = await _roleRepository.GetRoleWithPermissionsAsync(roleName);
            if (role != null && role.Permissions.Count > 0)
            {
                // Récupérer les claims existants de l'utilisateur
                var existingClaims = await _accountRepository.GetUserPermissionClaimsAsync(userId);

                // Ajouter seulement les permissions qui n'existent pas encore
                var toAdd = role.Permissions
                    .Select(p => p.Name)
                    .Where(name => !existingClaims.Contains(name))
                    .ToList();

                if (toAdd.Count > 0)
                {
                    await _accountRepository.AddClaimsAsync(user, toAdd);
                }
            }

            return Result<bool>.Success(true);
        }

        /// <inheritdoc />
        public async Task<Result<bool>> UnassignRoleAsync(string userId, string roleName)
        {
            var user = await _accountRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return Result<bool>.Fail("Utilisateur introuvable.");
            }

            // Retirer le rôle
            await _accountRepository.RemoveUserRole(user, roleName);

            // Récupérer les permissions du rôle retiré
            var role = await _roleRepository.GetRoleWithPermissionsAsync(roleName);
            if (role != null && role.Permissions.Count > 0)
            {
                // Récupérer les autres rôles de l'utilisateur
                var remainingRoles = await _accountRepository.GetUserRoles(user);

                // Récupérer les permissions des rôles restants
                var remainingPermissions = new HashSet<string>();
                foreach (var remainingRoleName in remainingRoles)
                {
                    var remainingRole = await _roleRepository.GetRoleWithPermissionsAsync(remainingRoleName);
                    if (remainingRole != null)
                    {
                        foreach (var p in remainingRole.Permissions)
                        {
                            remainingPermissions.Add(p.Name);
                        }
                    }
                }

                // Supprimer seulement les permissions qui ne sont plus couvertes par aucun rôle
                var toRemove = role.Permissions
                    .Select(p => p.Name)
                    .Where(name => !remainingPermissions.Contains(name))
                    .ToList();

                if (toRemove.Count > 0)
                {
                    await _accountRepository.RemoveClaimsAsync(user, toRemove);
                }
            }

            return Result<bool>.Success(true);
        }
    }
}

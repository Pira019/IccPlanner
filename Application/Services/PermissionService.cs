using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Shared.Constants;

namespace Application.Services
{
    /// <summary>
    ///     Service de gestion des permissions.
    /// </summary>
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly ILogger<PermissionService> _logger;

        /// <summary>
        ///     Liste des permissions par défaut du système.
        /// </summary>
        private static readonly List<(string Name, string Description, string Fnc)> DefaultPermissions =
        [
            // Roles
            (PermissionsConstants.CAN_READ_ROLE, PermissionsConstants.CAN_READ_ROLE_DESC, PermissionsConstants.CAT_ROLE),
            (PermissionsConstants.CAN_CREATE_ROLE, PermissionsConstants.CAN_CREATE_ROLE_DESC, PermissionsConstants.CAT_ROLE),
            (PermissionsConstants.CAN_MANAG_ROLES, PermissionsConstants.CAN_MANAG_ROLES_DESC, PermissionsConstants.CAT_ROLE),

            // Ministere
            (PermissionsConstants.CAN_CREATE_MINISTRY, PermissionsConstants.CAN_CREATE_MINISTRY_DESC, PermissionsConstants.CAT_MINISTRY),

            // Departement
            (PermissionsConstants.CAN_MANAG_DEPART, PermissionsConstants.CAN_MANAG_DEPART_DESC, PermissionsConstants.CAT_DEPT),
            (PermissionsConstants.DEPART_MANAGER, PermissionsConstants.DEPART_MANAGER_DESC, PermissionsConstants.CAT_DEPT),
            (PermissionsConstants.MANAGE_PRG_DETAILS, PermissionsConstants.MANAGE_PRG_DETAILS_DESC, PermissionsConstants.CAT_DEPT),

            // Programme
            (PermissionsConstants.PRG_MANAGER, PermissionsConstants.PRG_MANAGER_DESC, PermissionsConstants.CAT_PRG),

            // Service
            (PermissionsConstants.MANAGER_SERVICE, PermissionsConstants.MANAGER_SERVICE_DESC, PermissionsConstants.CAT_SERVICE),
        ];

        public PermissionService(IPermissionRepository permissionRepository, ILogger<PermissionService> logger)
        {
            _permissionRepository = permissionRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task SeedDefaultPermissionsAsync()
        {
            var existingNames = await _permissionRepository.GetAllNamesAsync();

            var toInsert = DefaultPermissions
                .Where(p => !existingNames.Contains(p.Name))
                .Select(p => new Permission
                {
                    Name = p.Name,
                    Description = p.Description,
                    Fnc = p.Fnc
                })
                .ToList();

            if (toInsert.Count > 0)
            {
                await _permissionRepository.InsertRangeAsync(toInsert);
                _logger.LogInformation("Permissions seed : {Count} permission(s) ajoutée(s).", toInsert.Count);
            }
        }
    }
}

using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.Extensions.Logging;

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
            // Rôles
            ("CanReadRole", "Peut lire un rôle", "Role"),
            ("CanCreateRole", "Peut créer un rôle", "Role"),

            // Ministère
            ("CanCreateMinistry", "Peut gérer un ministère", "Ministry"),

            // Département
            ("CanManagDepart", "Peut gérer un département (Créer, Supprimer et Modifier)", "Dept"),
            ("depart:manager", "Droit de gestion sur le département auquel l'utilisateur est rattaché", "Dept"),
            ("manage_program_details", "Permet de gérer les détails des programmes d'un département", "Dept"),

            // Programme
            ("prg:manager", "Peut gérer un programme (Créer, Supprimer et Modifier)", "Prg"),

            // Service
            ("ManagerService", "Peut gérer les services", "Service"),
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

namespace Application.Interfaces.Services
{
    /// <summary>
    ///     Interface pour le service de gestion des permissions.
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        ///     Seed les permissions par défaut du système.
        ///     Insère les permissions manquantes sans toucher aux existantes.
        /// </summary>
        Task SeedDefaultPermissionsAsync();
    }
}

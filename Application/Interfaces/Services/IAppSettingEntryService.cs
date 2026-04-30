using Application.Responses.Settings;

namespace Application.Interfaces.Services
{
    /// <summary>
    ///     Interface pour le service des paramètres de l'application.
    /// </summary>
    public interface IAppSettingEntryService
    {
        /// <summary>
        ///     Récupère tous les paramètres de délai (global, par programme, par département).
        /// </summary>
        Task<DeadlineSettingsResponse> GetDeadlineSettingsAsync();

        /// <summary>
        ///     Sauvegarde tous les paramètres de délai.
        /// </summary>
        Task SaveDeadlineSettingsAsync(SaveDeadlineSettingsRequest request);

        /// <summary>
        ///     Supprime une règle de délai par son Id.
        /// </summary>
        Task DeleteRuleAsync(int id);
    }
}

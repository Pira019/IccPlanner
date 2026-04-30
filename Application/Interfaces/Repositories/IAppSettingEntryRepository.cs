using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    ///     Interface pour le repository des paramètres de l'application.
    /// </summary>
    public interface IAppSettingEntryRepository
    {
        /// <summary>
        ///     Récupère tous les paramètres d'une catégorie.
        /// </summary>
        Task<List<AppSettingEntry>> GetByCategoryAsync(string category);

        /// <summary>
        ///     Récupère un paramètre par catégorie et clé.
        /// </summary>
        Task<AppSettingEntry?> GetAsync(string category, string key);

        /// <summary>
        ///     Crée ou met à jour un paramètre.
        /// </summary>
        Task UpsertAsync(AppSettingEntry entry);

        /// <summary>
        ///     Supprime un paramètre par son Id.
        /// </summary>
        Task DeleteAsync(int id);
    }
}

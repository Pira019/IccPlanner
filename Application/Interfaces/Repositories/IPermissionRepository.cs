using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    ///     Interface de permission.
    /// </summary>
    public interface IPermissionRepository : IBaseRepository<Permission>
    {
        /// <summary>
        ///     Récupère une liste de permissions en fonction d'une liste d'identifiants fournie.
        /// </summary>
        public Task<IEnumerable<Permission>> GetByIdsAsync(List<int> ids);

        /// <summary>
        ///     Récupère tous les noms de permissions existantes.
        /// </summary>
        public Task<List<string>> GetAllNamesAsync();

        /// <summary>
        ///     Insère plusieurs permissions en une seule opération.
        /// </summary>
        public Task InsertRangeAsync(List<Permission> permissions);
    }
}

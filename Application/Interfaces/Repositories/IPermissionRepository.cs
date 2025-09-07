using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    ///     Interface de permission.
    /// </summary>
    public interface IPermissionRepository : IBaseRepository<Permission>
    {
        /// <summary>
        ///     Récupère une liste d'identifiants de permissions en fonction d'une liste d'identifiants fournie.
        /// </summary>
        /// <param name="ids">
        ///     Liste des identifiants des permissions à récupérer.
        /// </param>
        /// <returns>
        ///     Retourne une collection d'entiers représentant les identifiants des permissions correspondantes.
        /// </returns>
        public Task<IEnumerable<Permission>> GetByIdsAsync(List<int> ids);
    }
}

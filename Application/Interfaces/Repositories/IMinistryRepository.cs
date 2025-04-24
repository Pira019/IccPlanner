using Application.Responses.Ministry;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IMinistryRepository : IBaseRepository<Ministry>
    {
        /// <summary>
        /// Vérifier si le nom du ministère existe
        /// </summary>
        /// <param name="name">Nom du ministère</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant la valeur <see cref="bool"/> de l'opération </returns>>
        public Task<bool> IsNameExists(string name);

        /// <summary>
        /// Permet de verifier si le ministère existe 
        /// </summary>
        /// <param name="id">Id du ministère</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant la valeur <see cref="bool"/> de l'opération </returns>>
        public Task<bool> IsExists(int id);

        /// <summary>
        /// Obtenir la liste de ministères
        /// </summary>
        /// <returns>
        /// Liste de ministère <see cref="GetMinistriesResponse"/> 
        /// </returns>
        public Task<IEnumerable<GetMinistriesResponse>> GetAll();
    }
}

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
    }
}

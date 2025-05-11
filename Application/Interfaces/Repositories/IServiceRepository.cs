using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    ///     
    /// </summary>
    public interface IServiceRepository : IBaseRepository<TabServices>
    {
        /// <summary>
        ///     Vérifie si un service existe deja par rapport au paramètre passé
        /// </summary>
        /// <param name="startTime">
        ///     Heure debut
        /// </param>
        /// <param name="endTime">
        ///     Heure fin
        /// </param>
        /// <param name="displayServiceName">
        ///     Nom d'affichage du service
        /// </param>
        /// <returns>
        ///     Retourne une <see cref="bool"/>
        /// </returns>
        public Task<bool> IsServiceExist(TimeOnly startTime, TimeOnly endTime, string displayServiceName);
    }
}

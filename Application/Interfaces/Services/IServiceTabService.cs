
namespace Application.Interfaces.Services
{
    public interface IServiceTabService : IBaseService
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
        public Task<bool> IsServiceExist(string startTime, string endTime, string displayServiceName); 
    }
}

// Ignore Spelling: Prg

using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    ///     Méthodes pour gérer les disponibilités des membres d'un département.    
    /// </summary>
    public interface IAvailabilityRepository : IBaseRepository<Availability>
    {
        /// <summary>
        ///    Vérifie si un membre a déjà choisi une disponibilité pour un service spécifique.
        /// </summary>
        /// <param name="servicePrgId"></param>
        ///     Id du programme de service pour lequel on veut vérifier la disponibilité.
        /// <param name="departmentMemberId">
        ///     Id du membre du département pour lequel on veut vérifier la disponibilité.
        /// </param>
        /// <returns>  
        ///     Une valeur booléenne indiquant si le membre a déjà choisi une disponibilité pour le service spécifié.
        /// </returns>
        public Task<bool> HasAlreadyChosenAvailability(int servicePrgId, int departmentMemberId);
    }
}

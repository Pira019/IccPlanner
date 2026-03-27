// Ignore Spelling: Prg

using Application.Dtos.AvailabilityDto;
using Application.Responses.Availability;
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

        /// <summary>
        ///     Retrouve l'ID d'un service programme spécifique.
        /// </summary>
        /// <param name="tabServicePrgId">
        ///     Id du service programme pour lequel on veut obtenir l'ID.
        /// </param>
        /// <param name="userId">
        ///     Id de l'utilisateur pour lequel on veut obtenir l'ID de la disponibilité.
        /// </param>
        /// <returns>
        ///     Retourne l'Id de la table Availability si trouvé, sinon null.
        /// </returns>
        public Task<GetAvailabityDto?> GetIdByAsync(int tabServicePrgId, Guid userId);

        /// <summary>
        ///     Récupère les TabServicePrgId déjà existants pour un membre donné parmi une liste.
        /// </summary>
        public Task<List<int>> GetExistingServicePrgIdsAsync(int departmentMemberId, List<int> servicePrgIds);

        /// <summary>
        ///     Récupère les disponibilités d'un utilisateur pour un mois donné.
        /// </summary>
        public Task<List<UserAvailabilityResponse>> GetUserAvailabilitiesAsync(Guid memberId, int month, int year, int departmentId);
    }
}

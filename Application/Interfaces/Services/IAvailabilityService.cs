using Application.Requests.Availability;

namespace Application.Interfaces.Services
{
    public interface IAvailabilityService : IBaseService
    {
        /// <summary>
        ///     Permet de récupérer l'ID du département d'un membre dans un département spécifique.
        /// </summary>
        /// <param name="authMemberGuid">
        ///     ID du membre authentifié.
        /// </param>
        /// <param name="idDepartment">
        ///     Id du département pour lequel on veut récupérer l'ID du membre.
        /// </param>
        /// <returns></returns>
        public Task<int?> GetIdDepartmentMember(Guid authMemberGuid, int? idDepartment);

        /// <summary>
        ///     Permet d'ajouter une disponibilité pour un membre dans un département spécifique.
        /// </summary>
        /// <param name="addAvailabilityRequest">
        ///     Objet contenant les informations de disponibilité à ajouter.
        /// </param>
        /// <param name="idDepartmentMember">
        ///     Id du membre dans le département pour lequel on veut ajouter la disponibilité.
        /// </param>
        /// <returns></returns>
        public Task<Object> Add( AddAvailabilityRequest addAvailabilityRequest, int idDepartmentMember); 
    }
}

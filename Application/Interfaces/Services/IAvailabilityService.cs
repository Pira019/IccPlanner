using Application.Requests.Availability;
using Application.Responses.Availability;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IAvailabilityService : IBaseService<Availability>
    {
        /// <summary>
        ///     Permet de récupérer l'ID du département d'un membre dans un département spécifique.
        /// </summary>
        public Task<int?> GetIdDepartmentMember(Guid authMemberGuid, int? idDepartment);

        /// <summary>
        ///     Permet d'ajouter une disponibilité pour un membre dans un département spécifique.
        /// </summary>
        public Task<Result<bool>> Add(AddAvailabilityRequest addAvailabilityRequest, int? idDepart);

        /// <summary>
        ///     Récupère les disponibilités d'un utilisateur pour un mois donné.
        /// </summary>
        public Task<Result<List<UserAvailabilityResponse>>> GetUserAvailabilitiesAsync(Guid memberId, int month, int year, int departmentId);

        /// <summary>
        ///     Récupère les membres disponibles par département et date, groupés par service.
        /// </summary>
        public Task<Result<List<AvailableMembersByDateResponse>>> GetAvailableMembersByDateAsync(int departmentId, DateOnly date);
    }
}

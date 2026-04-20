using Application.Responses.Member;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    ///     Interface pour gérer les membres de l'application.
    /// </summary>
    public interface IMemberRepository : IBaseRepository<Member>
    {
        /// <summary>
        ///     Obtient un membre par son identifiant de departement.
        /// </summary>
        /// <param name="departmentId">
        ///     Department Id du département pour lequel on veut obtenir le membre.
        /// </param>
        /// <returns></returns>
        public Task<MembersResponse> GetByDepartmentIdAsync(int departmentId);
        /// <summary>
        ///     Récupère le profil complet d'un membre par son identifiant Guid.
        /// </summary>
        public Task<ProfileResponse?> GetProfileAsync(Guid memberId);

        /// <summary>
        ///     Récupère un membre par son identifiant Guid.
        /// </summary>
        public Task<Member?> GetByGuidAsync(Guid memberId);
        /// <summary>
        ///     Récupère les anniversaires du mois pour les départements donnés.
        /// </summary>
        public Task<List<BirthdayResponse>> GetBirthdaysByMonthAsync(List<int> departmentIds, int month);
    } 
}

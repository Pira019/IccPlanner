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
    } 
}

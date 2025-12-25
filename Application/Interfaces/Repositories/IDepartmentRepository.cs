using System.Drawing;
using Application.Responses.Department;
using Application.Responses.TabService;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        /// <summary>
        /// Permet de verifier si le nom de département existe deja 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<bool> IsNameExistsAsync(string name);

        /// <summary>
        /// Si le département existe par Id
        /// </summary>
        /// <param name="id"> Id de département </param>
        /// <returns></returns>
        public Task<bool> IsDepartmentIdExists(int id);

        /// <summary>
        /// Permet de trouver un membre d'un département
        /// </summary>
        /// <param name="memberId"> Id du membre</param>
        /// <param name="departmentId"> Id du département </param>
        /// <returns></returns>
        public Task<DepartmentMember?> FindDepartmentMember(string memberId, int departmentId);
        public Task<DepartmentMember> SaveDepartmentMember(DepartmentMember departmentMember);
        public Task SaveDepartmentMemberPost(DepartmentMemberPost departmentMemberPost);

        /// <summary>
        /// Permet de retour les ids valides
        /// </summary>
        /// <param name="departmentIds"></param>
        /// <returns></returns>
        /// <remarks>
        /// La liste ne gère pas les doublons
        /// </remarks>
        public Task<IEnumerable<int?>> GetValidDepartmentIds(IEnumerable<int> departmentIds);  

        /// <summary>
        ///     Obtient la liste des services des départements d'un utilisateur par date
        /// </summary>
        /// <param name="dateOnly"></param>
        /// <returns></returns>
        public Task<IEnumerable<GetServicesListResponse>> GetDepartmentServicesByDate(Guid userId, DateOnly dateOnly);

        /// <summary>
        ///     Obtenir la liste des départements.
        /// </summary>
        /// <param name="membreId"></param>
        /// <returns>
        ///     Retourne une liste des départements.
        /// </returns>
        public Task<GetDepartResponse> GetDepartAsync(string? membreId, int pageNumber = 1, int pageSize = 50);
    }
}

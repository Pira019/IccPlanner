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
        ///     Obtient la liste des services de tous les départements pour une date donnée.
        /// </summary>
        /// <param name="dateOnly">Date du programme</param>
        /// <returns>Services groupés par département</returns>
        public Task<List<DepartmentServicesResponse>> GetDepartmentServicesByDateAsync(DateOnly datePrg);

        /// <summary>
        ///     Obtenir la liste des départements.
        /// </summary>
        /// <param name="membreId"></param>
        /// <returns>
        ///     Retourne une liste des départements.
        /// </returns>
        public Task<GetDepartResponse> GetDepartAsync(string? membreId, int? pageNumber, int? pageSize);

        /// <summary>
        ///     Récupère les postes associés à un département.
        /// </summary>
        /// <param name="departmentId">Id du département</param>
        /// <returns>Liste des postes</returns>
        public Task<List<PosteResponse>> GetPostesByDepartmentAsync(int departmentId);

        /// <summary>
        ///     Affecte une liste de postes à un département.
        /// </summary>
        public Task AssignPostesAsync(int departmentId, List<int> posteIds);

        /// <summary>
        ///     Récupère les détails complets d'un département (infos, membres, postes, programmes).
        /// </summary>
        public Task<DepartmentDetailResponse?> GetDetailAsync(int departmentId);

        /// <summary>
        ///     Affecte des postes à un membre du département (remplace les postes existants).
        /// </summary>
        /// <param name="departmentMemberId">Id du DepartmentMember</param>
        /// <param name="posteIds">Liste des ids de postes à affecter</param>
        public Task AssignPostesToMemberAsync(int departmentMemberId, List<int> posteIds);

        /// <summary>
        ///     Récupère des postes par leurs IDs.
        /// </summary>
        public Task<List<Poste>> GetPostesByIdsAsync(List<int> posteIds);

        /// <summary>
        ///     Récupère le userId et departmentId à partir d'un DepartmentMemberId.
        /// </summary>
        public Task<(string UserId, int DepartmentId)?> GetMemberInfoByDepartmentMemberIdAsync(int departmentMemberId);

        /// <summary>
        ///     Met à jour le flag IndPlanning sur un DepartmentMember.
        /// </summary>
        public Task UpdateDepartmentMemberIndPlanningAsync(int departmentMemberId, bool indPlanning);

        /// <summary>
        ///     Désaffecte un poste d'un département.
        /// </summary>
        public Task RemovePosteFromDepartmentAsync(int departmentId, int posteId);

        /// <summary>
        ///     Récupère le departmentId à partir d'un DepartmentProgramId.
        /// </summary>
        public Task<int?> GetDepartmentIdByDepartmentProgramIdAsync(int departmentProgramId);
    }
}

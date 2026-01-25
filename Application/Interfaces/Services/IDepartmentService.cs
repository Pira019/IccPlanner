using Application.Interfaces.Repositories;
using Application.Requests.Department;
using Application.Responses.Department;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IDepartmentService : IBaseService<Department>
    {
        /// <summary>
        /// Modèle de donner pour ajouter un département 
        /// </summary>
        /// <param name="addDepartmentRequest"><see cref="AddDepartmentResponse"/></param>
        /// <returns> <see cref="AddDepartmentResponse"/></returns>
        public Task<Result<AddDepartmentResponse>> AddDepartment(AddDepartmentRequest addDepartmentRequest);

        /// <summary>
        ///     Mettre a jour un département existant.
        /// </summary>
        /// <param name="id">
        ///     Identifiant du département à modifier.
        /// </param>
        /// <param name="addDepartmentRequest">
        ///     Modèle de donnée à recevoir pour la mise à jour.
        /// </param>
        /// <returns></returns>
        public Task<Result<bool>> UpdateDept(int id, AddDepartmentRequest addDepartmentRequest);

        /// <summary>
        /// Permet de savoir si le nom du département existe 
        /// </summary>
        /// <param name="name">Le nom du département </param>
        /// <returns></returns>
        public Task<bool> IsNameExists(string name);

        /// <summary>
        /// Permet d'ajouter un responsable du département
        /// </summary>
        /// <param name="addDepartmentRespoRequest"> Model de donnée a recevoir </param>
        /// <returns></returns>
        public Task AddDepartmentResponsable(AddDepartmentRespoRequest addDepartmentRespoRequest);

        /// <summary>
        /// Permet d'ajouter un programme dans un ou plusieurs départements 
        /// </summary>
        /// <param name="departmentProgramRequest"></param>
        /// <param name="userAuthId">Id de l'utilisateur connecter</param>
        /// <returns></returns>
        public Task<Result<bool>> AddDepartmentProgram (AddDepartmentProgramRequest departmentProgramRequest, string userId);

        /// <summary>
        /// Permet de supprimer un ou plusieurs DepartmentProgram 
        /// </summary>
        /// <param name="deleteDepartmentProgramRequest">Model du corps</param>
        /// <returns></returns>
        public Task DeleteDepartmentProgramByIdsAsync(DeleteDepartmentProgramRequest deleteDepartmentProgramRequest);

        /// <summary>
        /// Permet d'ajouter les member par le fichier excel
        /// </summary>
        /// <param name="addDepartmentMemberImportFileRequest"> Model de corps attendu</param>
        /// <param name="AuthenticatedUser"> L'Id de user qui a ajouter</param>
        /// <returns>Retourner le nombres des membres enregistrés</returns>
        public Task<int> ImportMembersAsync(AddDepartmentMemberImportFileRequest addDepartmentMemberImportFileRequest, Guid? AuthenticatedUser);

        /// <summary>
        /// Vérifie les ids qui existent dans DB 
        /// </summary>
        /// <param name="departmentIds"> Listes des Ids</param>
        /// <returns>
        /// Retourne une valeur <see cref="bool"/>
        /// </returns>
        /// <remarks>
        /// <code>True</code> si la liste existe
        /// </remarks>
        public Task<bool> IsValidDepartmentIds(IEnumerable<int> departmentIds);

        /// <summary>
        ///     Obtenir les départements.
        /// </summary>
        /// <param name="userAuthId">
        ///     Id de utilisateur connecté.
        /// </param>
        /// <param name="claimValues">
        ///     Verifier si l'utilisateur a une des claims
        /// </param>
        /// <param name="pageNumber">
        ///     
        /// </param>
        /// <returns>
        ///     Retourne un <see cref="GetDepartResponse"/>
        /// </returns>
        public Task<GetDepartResponse> GetAsync(string userAuthId, List<string> claimValues, int pageNumber = 1, int pageSize=50);

        /// <summary>
        ///     Determine un département par son Id.
        /// </summary>
        /// <param name="idDept"></param>
        /// <returns>
        ///     Retourne un <see cref="GetDepartResponse"/>
        /// </returns>
        public Task<DeptResponse> GetByIdAsync(int idDept);


    }
}

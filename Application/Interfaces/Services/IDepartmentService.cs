using Application.Requests.Department;
using Application.Responses.Department;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IDepartmentService
    {
        /// <summary>
        /// Modèle de donner pour ajouter un département 
        /// </summary>
        /// <param name="addDepartmentRequest"><see cref="AddDepartmentResponse"/></param>
        /// <returns> <see cref="AddDepartmentResponse"/></returns>
        public Task<AddDepartmentResponse> AddDepartment(AddDepartmentRequest addDepartmentRequest);

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
        public Task AddDepartmentsProgram(AddDepartmentProgramRequest departmentProgramRequest, Guid? userAuthId);

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
    }
}

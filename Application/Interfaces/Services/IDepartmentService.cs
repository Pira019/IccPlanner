using Application.Requests.Department;
using Application.Responses.Department;

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
    }
}

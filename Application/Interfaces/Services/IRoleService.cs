using Application.Dtos.Role;
using Application.Requests.Role;
using Application.Responses.Role;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Services
{
    /// <summary>
    /// Interface pour les actions des roles
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Permet de créer un Role
        /// </summary>
        /// <param name="createRoleRequest">Le model de la requête <see cref="CreateRoleRequest"/></param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="IdentityResult"/> de l'opération </returns>
        public Task<IdentityResult> CreateRole(CreateRoleRequest createRoleRequest);

        /// <summary>
        /// Récupérer tous les roles
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<GetRolesDto>> GetAll();

        /// <summary>
        /// Permet de récupéré Id d'un Role
        /// </summary>
        /// <param name="roleName">Nom du role</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant le result de l'opération </returns>
        public Task<CreateRoleResponse> GetRoleByName(string roleName);
        
    }
}

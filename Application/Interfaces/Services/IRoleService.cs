
using Application.Requests.Role;
using Application.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Services
{
    /// <summary>
    /// Interface pour les actions des roles
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Permet de creer un Role
        /// </summary>
        /// <param name="createRoleRequest">Le model de la requete <see cref="CreateRoleRequest"/></param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="IdentityResult"/> de l'opération </returns>
        public Task<IdentityResult> CreateRole(CreateRoleRequest createRoleRequest);

        /// <summary>
        /// Lire tous les Roles
        /// </summary>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="ApiListReponse{T}"/> de l'opération </returns>
        public Task<ApiListReponse<Role>> GetAllRoles();

        
    }
}

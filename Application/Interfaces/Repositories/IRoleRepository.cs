
using Application.Dtos.Role;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    /// Contrat Repository Role
    /// </summary> 
    public interface IRoleRepository
    {
        /// <summary>
        /// Créer un Role
        /// </summary>
        /// <param name="role">Nouveau Role <see cref="Role"/></param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="IdentityResult"/> de l'opération </returns>
        public Task<IdentityResult> CreateAsync(Role role);

        /// <summary>
        /// Récupérer tout les roles
        /// </summary>
        /// <returns></returns>
        public Task<List<GetRolesDto>> GetAllRoles();

        /// <summary>
        /// Récupérer l'Id d'un role
        /// </summary>
        /// <param name="roleName">Nom du role</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="Role"/> de l'opération </returns>
        public Task<Role?> GetRoleByNameAsync(string roleName);
    }
}

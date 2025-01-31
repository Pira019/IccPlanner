
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
        /// Créer uin Role
        /// </summary>
        /// <param name="role">Nouveau Role <see cref="Role"/></param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="IdentityResult"/> de l'opération </returns>
        public Task<IdentityResult> CreateAsync(Role role);
    }
}

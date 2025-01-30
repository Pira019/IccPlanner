
using Application.Requests.Role;

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
        /// <returns></returns>
        public Task CreateRole(CreateRoleRequest createRoleRequest);
        
    }
}

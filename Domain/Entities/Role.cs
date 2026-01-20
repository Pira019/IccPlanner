using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    /// <summary>
    ///      Représente les roles d'un utilisateur
    /// </summary>
    public class Role : IdentityRole
    {
        /// <summary>
        ///     Description du rôle.
        /// </summary>
        public required string Description { get; set; } 

        /// <summary>
        ///     Flag pour le role système.
        /// </summary>
        public bool? IndSys { get; set; } 

        /// <summary>
        ///     Liste des permissions associées à ce rôle.
        /// </summary>
        public List<Permission> Permissions { get; } = []; 
    }
}

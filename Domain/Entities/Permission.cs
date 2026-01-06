using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    // Clamis
    public class Permission
    {
        public int Id { get; set; }

        /// <summary>
        ///    Nom de la permission.
        /// </summary>
        [MaxLength(255)]
        public required string Name { get; set; }

        /// <summary>
        ///     Description de la permission.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Fonction  
        /// </summary>
        /// 
        [MaxLength(10)]
        public string? Fnc { get; set; }

        /// <summary>
        ///     Liste des rôles associés à cette permission.
        /// </summary>
        public List<Role> Roles { get; } = [];
    }
}

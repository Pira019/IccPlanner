using System.ComponentModel.DataAnnotations;
using Shared.Utiles;

namespace Domain.Entities
{
    /// <summary>
    /// Définit le poste qu'un membre peut avoir dans un département 
    /// </summary>
    public class Poste
    {
        private string _name = string.Empty;
        public int Id { get; set; }
        [MaxLength(55)]
        public required string Name { 
            get => _name;
            set => _name = SharedUtiles.CapitalizeFirstLetter(value);
        }
        public required string Description { get; set; }
        [MaxLength(15)]
        public string? ShortName { get; set; }

        /// <summary>
        ///     Indique si le poste confère le droit de gérer des membres dans le département.
        /// </summary>
        public bool IndGest { get; set; } = default;

        /// <summary>
        ///     Ordre d'affichage du poste (nullable).
        /// </summary>
        public int? DisplayOrder { get; set; }

        public List<Department> Departments { get; } = [];
    }
}

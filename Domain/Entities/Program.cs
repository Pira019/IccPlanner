using System.ComponentModel.DataAnnotations;
using Shared.Utiles;

namespace Domain.Entities
{
    /// <summary>
    /// Définit les jours des activité ex: cultes, soirée d'adoration etc
    /// </summary>
    public class Program : BaseEntity
    {
        private string _name = string.Empty;

        public int Id { get; set; }

        [MaxLength(55)]
        public required string Name { 
            get => _name;
            set => _name = SharedUtiles.CapitalizeFirstLetter(value); 
        }
        public string? Description { get; set; }
        [MaxLength(15)]
        public string? ShortName { get; set; }

        /// <summary>
        ///     Id de l'utilisateur ajouté.
        /// </summary>
        public string? AddBy { get; set; }

        /// <summary>
        ///     Id de l'utilisateur modifié.
        /// </summary>
        public string? UpdatedBy { get; set; }
        public List<Department> Departments { get; } = [];
        public List<DepartmentProgram> ProgramDepartments { get; } = [];
    }
}

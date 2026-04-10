using System.ComponentModel.DataAnnotations;
using Shared.Utiles;

namespace Domain.Entities
{
    public class Department : BaseEntity
    {
        private string _name = string.Empty;
        public int Id { get; set; }
        public int? MinistryId { get; set; }
        public Ministry? Ministry { get; set; }
        [MaxLength(255)]
        public required string Name { 
            get => _name;
            set => _name = SharedUtiles.CapitalizeFirstLetter(value);
        }
        public required string Description { get; set; }

        [MaxLength(15)]
        public string? ShortName { get; set; }
        public DateOnly? StartDate { get; set; } // Date d'ouverture
        public List<Member> Members { get; } = [];
        public List<DepartmentMember> DepartmentMembers { get; } = [];
        public List<Program> Programs { get; } = [];
        public List<DepartmentProgram> DepartmentPrograms { get; } = [];
        public List<Poste> Postes { get; } = [];

        /// <summary>
        ///     Nombre de jours à l'avance pour générer les dates récurrentes. Null = valeur globale.
        /// </summary>
        public int? RecurrentDaysAhead { get; set; }
    }
}

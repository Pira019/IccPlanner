
using System.ComponentModel.DataAnnotations;
using Shared.Utiles;

namespace Domain.Entities
{
    /// <summary>
    /// Classe ministère ex: TELECOMMUNICATION
    /// </summary>
    public class Ministry
    {
        private string _name = string.Empty;
        public int Id { get; set; }
        [MaxLength(255)]
        public required string Name { 
            get => _name;
            set => _name = SharedUtiles.CapitalizeFirstLetter(value);
        } 
        public required string Description { get; set; }
        public ICollection<Department> Departements { get; } = new List<Department>();
    }
}

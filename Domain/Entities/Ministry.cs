
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Classe ministère ex: TELECOMMUNICATION
    /// </summary>
    public class Ministry
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public required string Name { get; set; } 
        public required string Description { get; set; }
        public ICollection<Department> Departements { get; } = new List<Department>();
    }
}

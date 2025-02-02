
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Classe ministere ex: TELECOMMUNICATION
    /// </summary>
    public class Ministry
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; } 
        public string Description { get; set; }
        public ICollection<Departement> Departements { get; } = new List<Departement>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Definit les jours des activité ex: cultes, soiree d'adoration etc
    /// </summary>
    public class Program
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [MaxLength(50)]
        public string? ShortName { get; set; }
        public List<Departement> Departements { get; } = [];
    }
}

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Definit les jours des activité ex: cultes, soiree d'adoration etc
    /// </summary>
    public class Program
    {
        private int Id { get; set; }
        [MaxLength(255)]
        private string Name { get; set; }
        private string? Description { get; set; }
        [MaxLength(50)]
        private string? ShortName { get; set; }
        private List<Departement> Departements { get; } = [];
    }
}

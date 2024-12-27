namespace IccPlanner.Domain.Entities
{
    /// <summary>
    /// Definit les jours des activité ex: cultes, soiree d'adoration etc
    /// </summary>
    public class Program
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private string? Description { get; set; }
        private string ShortName { get; set; }
        private List<Departement> Departements { get; } = [];
    }
}

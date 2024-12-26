namespace IccPlanner.Domain.Entities
{
    /// <summary>
    /// Definit les jours des activité ex: cultes, soiree d'adoration etc
    /// </summary>
    public class Program
    {
        private Guid Id { get; set; }
        private string Name { get; set; }
        private string? Description { get; set; }
        private string ShortName { get; set; }
    }
}

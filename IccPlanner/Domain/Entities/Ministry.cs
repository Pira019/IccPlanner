namespace IccPlanner.Domain.Entities
{
    /// <summary>
    /// Classe ministere ex: TELECOMMUNICATION
    /// </summary>
    public class Ministry
    {
        private Guid Id { get;}
        private string Name { get; set; } 
        private string Description { get; set; }
    }
}

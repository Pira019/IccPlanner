namespace IccPlanner.Domain.Entities
{
    /// <summary>
    /// Disponibilté des member
    /// </summary>
    public class Availability : BaseEntity
    {
        private Guid Id { get; set; }
        private bool IsAvailable { get; set; }  
        private string? Comment { get; set; }
    }
}

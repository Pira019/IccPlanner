namespace IccPlanner.Domain.Entities
{
    public class Profile : BaseEntity
    {
        private Guid Id { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }    
    }
}

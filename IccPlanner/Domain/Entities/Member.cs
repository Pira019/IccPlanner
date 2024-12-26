using IccPlanner.Domain.Enums;

namespace IccPlanner.Domain.Entities
{
    /// <summary>
    /// Cette clase definit un member, ex: un chantre etc ...
    /// </summary>
    public class Member : Person
    {
        private Guid Id { get; set; }
        private DateOnly? EntryDate { get; set; }
        private Member AddedBy { get; set; } 
        private DateOnly? BirthDate { get; set; } 
        private MemberStatusEnum Status { get; set; } 
    }
}

using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Cette clase definit un member, ex: un chantre etc ...
    /// </summary>
    public class Member : Person
    {
        private Guid Id { get; set; }
        private User User { get; set; } 
        private DateOnly? EntryDate { get; set; }
        private Guid AddedByMemberId { get; set; } // Id member
        private Member AddedBy { get; set; } //  member
        private DateOnly? BirthDate { get; set; } 
        private MemberStatusEnum Status { get; set; }
        private List<Departement> Departements { get; } = [];
    }
}

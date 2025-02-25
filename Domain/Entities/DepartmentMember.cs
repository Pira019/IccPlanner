using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    public class DepartmentMember : BaseEntity
    {
        public int Id { get; set; }
        public Guid MemberId { get; set; }
        public int DepartementId { get; set; }
        public Member Member { get; set; } = null!;
        public Department Departement { get; set; } = null!;
        [MaxLength(55)]
        public string? NickName {  get; set; }
        public DateOnly? DateEntry { get; set; }
        public MemberStatus Status { get; set; } = MemberStatus.Active;      
        public List<ProgramDepartment> ProgramDepartments { get; } = [];
        public List<Poste> Postes { get; } = [];
        List<DepartmentMemberPost> DepartmentMemberPosts { get; } = [];
        public List<FeedBack> FeedBacks { get; } = [];
        public List<Availability> Availabilities { get; } = [];
    }
}

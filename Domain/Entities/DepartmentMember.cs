using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    public class DepartmentMember : BaseEntity
    {
        public int Id { get; set; }
        public Guid MemberId { get; set; }
        public int DepartmentId { get; set; }
        public Member Member { get; set; } = null!;
        public Department Department { get; set; } = null!;
        [MaxLength(55)]
        public string? NickName {  get; set; }
        public DateOnly? DateEntry { get; set; }
        public MemberStatus Status { get; set; } = MemberStatus.Active;      
        public List<DepartmentProgram> ProgramDepartments { get; } = [];
        public List<Poste> Postes { get; } = [];
        public List<DepartmentMemberPost> DepartmentMemberPosts { get; } = [];
        public List<FeedBack> FeedBacks { get; } = [];
        public List<Availability> Availabilities { get; } = [];
    }
}

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
        public Departement Departement { get; set; } = null!;
        [MaxLength(55)]
        public string? NickName {  get; set; }
        public DateOnly? DateEntry { get; set; }
        public MemberStatus Staus { get; set; }       
        public List<ProgramDepartment> ProgramDepartments { get; } = [];
        public List<FeedBack> FeedBacks { get; } = [];
        public List<Availability> Availabilities { get; } = [];


    }
}

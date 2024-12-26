using IccPlanner.Domain.Enums;

namespace IccPlanner.Domain.Entities
{
    public class DepartmentMember : BaseEntity
    {
        private Member  Member { get; set; }
        private Departement Departement { get; set; } 
        private string NickName {  get; set; }
        private DateOnly DateEnty { get; set; }
        private MemberStatusEnum Staus { get; set; }


    }
}

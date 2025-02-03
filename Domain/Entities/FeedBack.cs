using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class FeedBack : BaseEntity
    {
        public int Id {  get; set; }
        public int DepartmentMemberId { get; set; } 
        public int ProgramDepartmentId { get; set; }
        public DepartmentMember DepartmentMember { get; set; } = null!;
        public ProgramDepartment ProgramDepartment { get; set; } = null!;
        public required string Comment { get; set; }
        public int ? Rating { get; set; }
        public DateTime SubmitAt { get; set; }
    }
}

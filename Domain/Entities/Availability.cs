namespace Domain.Entities
{
    /// <summary>
    /// Disponibilté des member
    /// </summary>
    public class Availability : BaseEntity
    { 
        public int Id { get; set; }
        public int ProgramId { get; set; } // ProgramDepartment
        public int MemberId { get; set; } //DepartmentMember
        public ProgramDepartment ProgramDepartment { get; set; } = null!;
        public DepartmentMember DepartmentMember { get; set; } = null!;
        public bool IsAvailable { get; set; }  
        public string? Comment { get; set; }
    }
}

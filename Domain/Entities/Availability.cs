namespace Domain.Entities
{
    /// <summary>
    /// Disponibilté des member
    /// </summary>
    public class Availability : BaseEntity
    { 
        public int Id { get; set; }
        public int ProgramId { get; set; } // DepartmentProgram
        public int MemberId { get; set; } //DepartmentMember
        public DepartmentProgram ProgramDepartment { get; set; } = null!;
        public DepartmentMember DepartmentMember { get; set; } = null!;
        public bool IsAvailable { get; set; }  
        public string? Comment { get; set; }
    }
}

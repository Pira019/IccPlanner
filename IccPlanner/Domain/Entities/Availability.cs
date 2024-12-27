namespace IccPlanner.Domain.Entities
{
    /// <summary>
    /// Disponibilté des member
    /// </summary>
    public class Availability : BaseEntity
    {
        private int Id { get; set; }
        private int ProgramDepartmentId { get; set; }
        private int DepartmentMemberId { get; set; }
        private ProgramDepartment ProgramDepartment { get; set; } = null!;
        private DepartmentMember DepartmentMember { get; set; } = null!;
        private bool IsAvailable { get; set; }  
        private string? Comment { get; set; }
    }
}

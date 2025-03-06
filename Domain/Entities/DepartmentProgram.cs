using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Classe qui définit le programme
    /// </summary>
    public class DepartmentProgram : BaseEntity
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int ProgramId { get; set; }
        public Program Program { get; set; } = null!;
        public  Department Department { get; set; } = null!;
        public DateOnly StartAt { get; set; }  
        [MaxLength(255)]
        public string? Localisation { get; set; }
        public string? Comment { get; set; } 
        public required Guid CreateById { get; set; }
        public Member CreateBy { get; set; } = null!;
        public Member? UpdateBy { get; set; }
        public bool IsRecurring { get; set; } = false;
        public List<DepartmentMember> DepartmentMembers { get; } = [];
        public List<FeedBack>? FeedBacks{ get; set; }
        public List<Availability> Availabilities { get; } = [];


    }
}

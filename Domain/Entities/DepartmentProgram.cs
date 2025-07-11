// Ignore Spelling: Prg

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
        public PrgDepartmentInfo? PrgDepartmentInfo { get; set; }
        public Program Program { get; set; } = null!;
        public  Department Department { get; set; } = null!;        
        [MaxLength(25)]
        public required string Type { get; set; }       
        public string? Comment { get; set; } 
        public required Guid CreateById { get; set; }
        public Member? CreateBy { get; set; } = null!;
        public Member? UpdateBy { get; set; }
        public List<FeedBack>? FeedBacks{ get; set; }


    }
}

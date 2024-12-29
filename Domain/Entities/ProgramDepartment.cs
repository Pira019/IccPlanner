using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Classe qui définit le programme
    /// </summary>
    public class ProgramDepartment : BaseEntity
    {
        public int Id { get; set; }
        public int DepartementId { get; set; }
        public int ProgramId { get; set; }
        public Program Program { get; set; } 
        public Departement Departement { get; set; } 
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        [MaxLength(255)]
        public string? Localisation { get; set; }
        public string? Comment { get; set; }
        public Member CreateBy { get; set; }
        public Member? UpdateBy { get; set; }
        public bool? isRecurring { get; set; } = false;
        public List<DepartmentMember> DepartmentMembers { get; } = [];
        public List<FeedBack> FeedBacks { get; }
        public List<Availability> Availabilities { get; } = [];


    }
}

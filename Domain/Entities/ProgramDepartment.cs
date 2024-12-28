using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Classe qui définit le programme
    /// </summary>
    public class ProgramDepartment : BaseEntity
    {
        private int Id { get; set; }
        private int DepartementId { get; set; }
        private int ProgramId { get; set; }
        private DateTime StartAt { get; set; }
        private DateTime EndAt { get; set; }
        [MaxLength(255)]
        private string? Localisation { get; set; }
        private string? Comment { get; set; }
        private Member CreateBy { get; set; }
        private Member? UpdateBy { get; set; }
        private bool? isRecurring { get; set; } = false;
        private List<DepartmentMember> DepartmentMembers { get; } = [];
        private List<FeedBack> FeedBacks { get; }
        private List<Availability> Availabilities { get; } = [];


    }
}

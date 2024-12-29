using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{

    /// <summary>
    /// Planning 
    /// </summary>
    public class Planning
    {   
        public int Id { get; set; }
        public int AvailabilityId { get; set; }
        [MaxLength(255)]
        public string MemberName { get; set; }
        public DateOnly ProgramDate { get; set; } // ProgramDepartment
        public Availability Availability { get; set; } = null!;
        public Guid ProgrammedById { get; set; } // Member Id
        public Member ProgrammedBy { get; set; } = null!;
        public Guid? UpdatedById { get; set; } // Member Id
        public Member? UpdatedBy { get; set; }
        public string? Comment { get; set; } 
        public PlanningTypeEnum PlanningType { get; set; } // ex Formation, Observation etc
    }
}

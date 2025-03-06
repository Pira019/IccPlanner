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
        public required string MemberName { get; set; }
        public DateOnly ProgramDate { get; set; } // DepartmentProgram
        public Availability Availability { get; set; } = null!;
        public Guid ProgrammedById { get; set; } // Member Id
        public Member ProgrammedBy { get; set; } = null!;
        public Guid? UpdatedById { get; set; } // Member Id
        public Member? UpdatedBy { get; set; }
        public string? Comment { get; set; } 
        public PlanningType PlanningType { get; set; } // ex Formation, Observation etc
    }
}

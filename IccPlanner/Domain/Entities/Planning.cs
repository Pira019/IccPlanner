namespace IccPlanner.Domain.Entities
{

    /// <summary>
    /// Planning 
    /// </summary>
    public class Planning
    {   
        public int AvailabilityId { get; set; }
        public string MemberName { get; set; }
        public Availability Availability { get; set; } = null!;
        public Guid ProgramedById { get; set; } // Member Id
        public Member ProgrammedBy { get; set; } = null!;
        public Guid? UpdatedById { get; set; } // Member Id
        public Member? UpdatedBy { get; set; }
        public string Comment { get; set; } 
    }
}

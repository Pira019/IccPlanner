namespace Application.Responses.Availability
{
    public class AvailableMembersByDateResponse
    {
        public int ServicePrgId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string ProgramName { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string? ArrivalTime { get; set; }
        public List<AvailableMemberItem> AvailableMembers { get; set; } = [];
    }

    public class AvailableMemberItem
    {
        public int AvailabilityId { get; set; }
        public Guid MemberId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public bool IsPlanned { get; set; }
        public bool IsTraining { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

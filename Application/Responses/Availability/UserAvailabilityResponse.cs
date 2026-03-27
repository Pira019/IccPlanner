namespace Application.Responses.Availability
{
    public class UserAvailabilityResponse
    {
        public DateOnly? Date { get; set; }
        public List<UserAvailabilityItem> Items { get; set; } = [];
    }

    public class UserAvailabilityItem
    {
        public int AvailabilityId { get; set; }
        public int TabServicePrgId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string ProgramName { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string? ArrivalTime { get; set; }
    }
}

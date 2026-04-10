namespace Application.Requests.ServiceTab
{
    public class UpdateServicePrgRequest
    {
        public int? TabServicesId { get; set; }
        public string? DisplayName { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? ArrivalTimeOfMember { get; set; }
        public string? Notes { get; set; }
    }
}

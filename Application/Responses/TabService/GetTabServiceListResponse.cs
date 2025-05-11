namespace Application.Responses.TabService
{
    public class GetTabServiceListResponse
    {
        public int id { get; set; }
        public required string DisplayName { get; set; } 
        public string? Comment { get; set; } 
        public string? MemberArrivalTime { get; set; } 
        public required string StartTime { get; set; } 
        public required string EndTime { get; set; } 
    }
}

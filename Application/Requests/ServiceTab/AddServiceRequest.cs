
namespace Application.Requests.ServiceTab
{
    /// <summary>
    /// Model de donnée pour ajouter un service
    /// </summary>
    public class AddServiceRequest
    {
        public required String StartTime { get; set; } 
        public required String EndTime { get; set; } 
        public String? MemberArrivalTime { get; set; } 
        public required string DisplayName { get; set; }
        public string? Comment { get; set; }
    }
}

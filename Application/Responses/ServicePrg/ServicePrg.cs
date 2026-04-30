
namespace Application.Responses.ServicePrg
{
    public class ServicePrg
    {
       public int Id { get; set; }
       public bool IsAvailable { get; set; }
       public string DisplayName { get; set; } = string.Empty;
       public string? Comment { get; set; }
       public string StartTime { get; set; } = string.Empty;
       public string EndTime { get; set; } = string.Empty;
       public string ArrivalTime { get; set; } = string.Empty;
    }
}

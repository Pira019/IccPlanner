
namespace Application.Dtos.TabServicePrgDto
{
    /// <summary>
    ///   Représente un programme de service.
    /// </summary>
    public class ServiceProgramDto
    {
        public int? ServiceProgramId { get; set; } = default!;
        public string? ProgramName { get; set; } = default!;
        public string? ProgramShortName { get; set; } 
        public string? DisplayName { get; set; } 
        public string? ServantArrivalTime { get; set; }  
        public TimeOnly? StartTime { get; set; }  
        public string? EndTime { get; set; }  
        public bool? IsAvailable { get; set; }  

    }
}

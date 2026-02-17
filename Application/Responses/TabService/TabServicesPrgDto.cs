namespace Application.Responses.TabService
{
    public class TabServicesPrgDto
    {
        public int IdTabService { get; set; } 
        public string ServiceTitle { get; set; } 
        public TimeOnly StartTime { get; set; } 
        public TimeOnly EndTime { get; set; } 
        public TimeOnly? ArrivalTime { get; set; } 
    }
}

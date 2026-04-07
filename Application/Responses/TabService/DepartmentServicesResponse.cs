namespace Application.Responses.TabService
{
    /// <summary>
    ///     Services groupés par département pour une date donnée.
    /// </summary>
    public class DepartmentServicesResponse
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string? DepartmentShortName { get; set; }
        public List<DepartmentServiceItem> Services { get; set; } = [];
    }

    public class DepartmentServiceItem
    {
        public int ServicePrgId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string ProgramName { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string? ArrivalTime { get; set; }
    }
}

namespace Application.Responses.Planning
{
    public class MyPlanningResponse
    {
        public DateOnly Date { get; set; }
        public string ProgramName { get; set; } = string.Empty;
        public string? ProgramShortName { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string? PosteName { get; set; }
        public bool IndTraining { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
    }
}

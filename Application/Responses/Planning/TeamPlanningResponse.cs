namespace Application.Responses.Planning
{
    public class TeamPlanningResponse
    {
        public DateOnly Date { get; set; }
        public string ProgramName { get; set; } = string.Empty;
        public string? ProgramShortName { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public string? PosteName { get; set; }
        public bool IndTraining { get; set; }
    }
}

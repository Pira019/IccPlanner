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

        /// <summary>
        ///     Autres membres programmés pour le même service/date.
        /// </summary>
        public List<MyPlanningTeammate> Teammates { get; set; } = [];
    }

    /// <summary>
    ///     Coéquipier programmé pour le même service/date.
    /// </summary>
    public class MyPlanningTeammate
    {
        public string MemberName { get; set; } = string.Empty;
        public string? PosteName { get; set; }
        public bool IndTraining { get; set; }
    }
}

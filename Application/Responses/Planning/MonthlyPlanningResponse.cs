namespace Application.Responses.Planning
{
    /// <summary>
    ///     Réponse allégée du récapitulatif mensuel.
    ///     Groupé par Programme → Date → Service → Membres.
    ///     Le département est déjà filtré côté requête.
    /// </summary>
    public class MonthlyPlanningResponse
    {
        public string ProgramName { get; set; } = string.Empty;
        public string? ProgramShortName { get; set; }
        public List<PlanningDateResponse> Dates { get; set; } = [];
    }

    public class PlanningDateResponse
    {
        public DateOnly Date { get; set; }
        public List<PlanningServiceResponse> Services { get; set; } = [];
    }

    public class PlanningServiceResponse
    {
        public string ServiceName { get; set; } = string.Empty;
        public List<PlannedMemberResponse> Members { get; set; } = [];
    }

    public class PlannedMemberResponse
    {
        public int PlanningId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public string? PosteName { get; set; }
        public bool IndTraining { get; set; }
    }
}

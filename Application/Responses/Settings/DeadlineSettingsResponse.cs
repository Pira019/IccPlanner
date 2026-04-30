namespace Application.Responses.Settings
{
    /// <summary>
    ///     Réponse contenant tous les paramètres de délai.
    /// </summary>
    public class DeadlineSettingsResponse
    {
        public int GlobalDeadline { get; set; } = 3;
        public string GlobalUnit { get; set; } = "days";
        public List<DeadlineRuleResponse> ProgramRules { get; set; } = [];
        public List<DeadlineRuleResponse> DepartmentRules { get; set; } = [];
    }

    public class DeadlineRuleResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Deadline { get; set; }
        public string Unit { get; set; } = "days";
    }

    /// <summary>
    ///     Requête pour sauvegarder les paramètres de délai.
    /// </summary>
    public class SaveDeadlineSettingsRequest
    {
        public int GlobalDeadline { get; set; }
        public string GlobalUnit { get; set; } = "days";
        public List<SaveDeadlineRuleRequest> ProgramRules { get; set; } = [];
        public List<SaveDeadlineRuleRequest> DepartmentRules { get; set; } = [];
    }

    public class SaveDeadlineRuleRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Deadline { get; set; }
        public string Unit { get; set; } = "days";
    }
}

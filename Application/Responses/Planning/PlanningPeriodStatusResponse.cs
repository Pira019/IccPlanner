namespace Application.Responses.Planning
{
    public class PlanningPeriodStatusResponse
    {
        public bool IndPublished { get; set; }
        public bool IndArchived { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}

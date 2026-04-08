namespace Application.Requests.Planning
{
    public class UpdatePlanningRequest
    {
        public int? PosteId { get; set; }
        public string? Comment { get; set; }
        public bool IndTraining { get; set; } = false;
        public bool IndObservation { get; set; } = false;
    }
}

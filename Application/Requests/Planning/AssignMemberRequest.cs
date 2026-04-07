namespace Application.Requests.Planning
{
    public class AssignMemberRequest
    {
        public int AvailabilityId { get; set; }
        public int? PosteId { get; set; }
        public string? Comment { get; set; }
        public bool IndTraining { get; set; } = false;
        public bool IndObservation { get; set; } = false;
        public bool ForceAssign { get; set; } = false;
    }
}

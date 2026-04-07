using Application.Dtos.Planning;

namespace Application.Responses.Planning
{
    public class AssignMemberResponse
    {
        public int? PlanningId { get; set; }
        public bool IsWarning { get; set; } = false;
        public string? Message { get; set; }
        public List<OverlapConflictDto>? Conflicts { get; set; }
    }
}

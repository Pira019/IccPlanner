namespace Application.Dtos.Planning
{
    public class OverlapConflictDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
    }
}

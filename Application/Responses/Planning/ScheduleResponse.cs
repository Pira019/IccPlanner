namespace Application.Responses.Planning
{
    public class ScheduleResponse
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public List<ScheduleWeekResponse> Weeks { get; set; } = [];
    }

    public class ScheduleWeekResponse
    {
        public int WeekNumber { get; set; }
        public List<ScheduleColumnResponse> Columns { get; set; } = [];
        public List<SchedulePosteRowResponse> Postes { get; set; } = [];
    }

    public class ScheduleColumnResponse
    {
        public string Key { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public string DayLabel { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
    }

    public class SchedulePosteRowResponse
    {
        public string PosteName { get; set; } = string.Empty;
        public Dictionary<string, List<ScheduleMemberResponse>> Cells { get; set; } = new();
    }

    public class ScheduleMemberResponse
    {
        public string MemberName { get; set; } = string.Empty;
        public bool IndTraining { get; set; }
    }
}

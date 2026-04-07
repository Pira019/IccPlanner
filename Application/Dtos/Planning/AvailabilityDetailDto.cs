namespace Application.Dtos.Planning
{
    /// <summary>
    ///     Détails extraits d'une disponibilité pour créer un planning.
    /// </summary>
    public class AvailabilityDetailDto
    {
        public int DepartmentId { get; set; }
        public Guid MemberId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateOnly ProgramDate { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}

namespace IccPlanner.Domain.Entities
{
    public class ProgramDepartment : BaseEntity
    {
        private Guid Id { get; set; }
        private Departement Departement { get; set; }
        private Program Program { get; set; }
        private DateTime StartAt { get; set; }
        private DateTime EndAt { get; set; }
        private string? Localisation { get; set; }
        private string? Comment { get; set; }
        private Member CreateBy { get; set; }
        private Member? UpdateBy { get; set; }
        private bool? isRecurring { get; set; } = false;

    }
}

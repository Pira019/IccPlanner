using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    ///     Snapshot d'un planning publié.
    ///     Copié depuis Planning au moment de la publication.
    ///     Les membres lisent cette table, pas Planning.
    /// </summary>
    public class PublishedPlanning
    {
        public int Id { get; set; }
        public int PlanningPeriodId { get; set; }
        public PlanningPeriod PlanningPeriod { get; set; } = null!;
        public int SourcePlanningId { get; set; }
        [MaxLength(255)]
        public required string MemberName { get; set; }
        public Guid MemberId { get; set; }
        public DateOnly ProgramDate { get; set; }
        public string ProgramName { get; set; } = string.Empty;
        public string? ProgramShortName { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string? PosteName { get; set; }
        public bool IndTraining { get; set; }
        public bool IndObservation { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}

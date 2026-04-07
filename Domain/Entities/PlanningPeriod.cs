namespace Domain.Entities
{
    /// <summary>
    ///     Représente un planning mensuel pour un département.
    /// </summary>
    public class PlanningPeriod
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public int Month { get; set; }
        public int Year { get; set; }

        /// <summary>
        ///     Indique si le planning est publié.
        /// </summary>
        public bool IndPublished { get; set; } = false;

        /// <summary>
        ///     Indique si le planning est archivé (lecture seule).
        /// </summary>
        public bool IndArchived { get; set; } = false;

        public DateTime? PublishedAt { get; set; }
        public Guid? PublishedById { get; set; }
        public Member? PublishedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Planning> Plannings { get; } = [];
    }
}

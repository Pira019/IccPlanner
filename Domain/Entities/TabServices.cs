// Ignore Spelling: Prg

namespace Domain.Entities
{
    public class TabServices
    {
        public int Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        /// <summary>
        /// Heure d'arriver des membres
        /// </summary>
        public TimeOnly? ArrivalTimeOfMember { get; set; }
        public int PrgDateId { get; set; }
        public PrgDate PrgDate { get; set; }   = null!;

        /// <summary>
        /// Text a afficher
        /// </summary>
        public required string DisplayName { get; set; }
        public string? Notes { get; set; }

    }
}

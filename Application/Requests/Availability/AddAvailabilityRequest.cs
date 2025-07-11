// Ignore Spelling: Prg

namespace Application.Requests.Availability
{
    /// <summary>
    /// Model de donnée pour ajouter une programme
    /// </summary>
    public class AddAvailabilityRequest
    {
        public int ServicePrgId { get; set; }
        public string? Notes { get; set; }
    }
}

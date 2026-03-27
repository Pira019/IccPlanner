// Ignore Spelling: Prg

namespace Application.Requests.Availability
{
    /// <summary>
    /// Model de donnée pour ajouter une ou plusieurs disponibilités
    /// </summary>
    public class AddAvailabilityRequest
    {
        public List<int> ServicePrgIds { get; set; } = new();
        public string? Notes { get; set; }
    }
}

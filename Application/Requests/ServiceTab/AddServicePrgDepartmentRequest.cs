// Ignore Spelling: Prg

namespace Application.Requests.ServiceTab
{
    /// <summary>
    ///     Permet de persister un service d"un programme d"un département
    /// </summary>
    public class AddServicePrgDepartmentRequest
    {
        public required int ServiceId { get; set; }
        public required int PrgDateId { get; set; }
        public String? MemberArrivalTime { get; set; } 
        public String? DisplayName { get; set; }
        public String?  Notes { get; set; }
    }
}

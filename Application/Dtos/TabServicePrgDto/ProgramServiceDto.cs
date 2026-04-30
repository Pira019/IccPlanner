
using Application.Responses.TabService;

namespace Application.Dtos.TabServicePrgDto
{
    /// <summary>
    ///   Représente un programme de service.
    /// </summary>
    public class ProgramServiceDto
    {
        public int IdPrg { get; set; }
        public int ProgramId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ShortName { get; set; }
        public string? Description { get; set; }
        public IEnumerable<TabServicesPrgDto> Services { get; set; } = [];

    }
}

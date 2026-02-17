
using Application.Responses.TabService;

namespace Application.Dtos.TabServicePrgDto
{
    /// <summary>
    ///   Représente un programme de service.
    /// </summary>
    public class ProgramServiceDto
    {
        public int IdPrg { get; set; }
        public string Title { get; set; } 
        public string? ShortName { get; set; } 
        public IEnumerable<TabServicesPrgDto> Services { get; set; }

    }
}

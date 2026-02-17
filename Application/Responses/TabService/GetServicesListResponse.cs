using Application.Dtos.TabServicePrgDto;

namespace Application.Responses.TabService
{
    public class GetServicesListResponse
    {
        public string GroupKey { get; set; }
        public int IdPrgDate { get; set; }
        public List<ProgramServiceDto> ServicePrograms { get; set; }
    }
}

using Application.Dtos.TabServicePrgDto;

namespace Application.Responses.TabService
{
    public class GetServicesListResponse
    {
        public string? DepartmentName { get; set; }
        public IEnumerable<ServiceProgramDto> ServicePrograms { get; set; } = default!;
    }
}

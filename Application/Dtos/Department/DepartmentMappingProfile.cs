
using Application.Requests.Department;
using Application.Responses.Department;
using AutoMapper;

namespace Application.Dtos.Department
{
    public class DepartmentMappingProfile : Profile
    { 
        public DepartmentMappingProfile()
        {
            CreateMap<AddDepartmentRequest, Domain.Entities.Department>();
            CreateMap<Domain.Entities.Department, AddDepartmentResponse>();
        }
    }
}

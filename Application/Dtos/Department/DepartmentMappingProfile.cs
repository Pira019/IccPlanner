
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

            CreateMap<AddDepartmentRespoRequest, Domain.Entities.DepartmentMember>()
                .ForMember(dest => dest.DepartementId, opt => opt.MapFrom(src => src.DepartmentId));

            CreateMap<AddDepartmentRespoRequest, Domain.Entities.DepartmentMemberPost>()               
                .ForMember(dest => dest.PosteId, opt => opt.Ignore())
                .ForMember(dest => dest.DepartmentMemberId, opt => opt.Ignore());
        }
    }
}

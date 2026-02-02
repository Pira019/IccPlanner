using Application.Helper;
using Application.Requests.Department;
using Application.Responses.Department;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos.Department
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<AddDepartmentRequest, Domain.Entities.Department>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Ministry, opt => opt.Ignore())
                    .ForMember(dest => dest.Members, opt => opt.Ignore())
                    .ForMember(dest => dest.Programs, opt => opt.Ignore())
                    .ForMember(dest => dest.DepartmentMembers, opt => opt.Ignore())
                    .ForMember(dest => dest.DepartmentPrograms, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<Domain.Entities.Department, AddDepartmentResponse>();
            CreateMap<Domain.Entities.Department, DeptResponse>();

            CreateMap<AddDepartmentRespoRequest, Domain.Entities.DepartmentMember>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Member, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.NickName, opt => opt.Ignore())
                .ForMember(dest => dest.DateEntry, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.DepartmentMemberPosts, opt => opt.Ignore())
                .ForMember(dest => dest.Postes, opt => opt.Ignore())
                .ForMember(dest => dest.FeedBacks, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<AddDepartmentRespoRequest, Domain.Entities.DepartmentMemberPost>()
                .ForMember(dest => dest.PosteId, opt => opt.Ignore())
                .ForMember(dest => dest.DepartmentMemberId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Poste, opt => opt.Ignore())
                .ForMember(dest => dest.DepartmentMember, opt => opt.Ignore());

            CreateMap<AddDepartmentProgramRequest, DepartmentProgram>()
                .ForMember(dest => dest.DepartmentId, opt => opt.Ignore())
                .ForMember(dest => dest.Program, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.CreateBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdateBy, opt => opt.Ignore())
                .ForMember(dest => dest.IndRecurent, opt => opt.Ignore())
                .ForMember(dest => dest.FeedBacks, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreateBy, opt => opt.Ignore()); 
        }
    }
}

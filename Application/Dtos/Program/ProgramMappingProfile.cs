using Application.Requests.Program; 
using Application.Responses.Program;
using AutoMapper;

namespace Application.Dtos.Program
{
    public class ProgramMappingProfile : Profile
    {
        public ProgramMappingProfile()
        {
            CreateMap<AddProgramRequest, Domain.Entities.Program>(); 

            CreateMap<Domain.Entities.Program, AddProgramResponse>()
                .ForMember(dest => dest.ProgramId, opt => opt.MapFrom(src => src.Id)); 
        } 
    }
}

using Application.Requests.Availability;
using Application.Requests.ServiceTab;
using Application.Responses;
using Application.Responses.TabService;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos.TabServiceMap
{
    public class AvailabilityMappingProfile : Profile
    {
        public AvailabilityMappingProfile()
        {
            CreateMap<AddAvailabilityRequest, Availability>()
             .ForMember(dest => dest.TabServicePrgId, opt => opt.MapFrom(src => src.ServicePrgId))
             .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes));
        }
    }
}

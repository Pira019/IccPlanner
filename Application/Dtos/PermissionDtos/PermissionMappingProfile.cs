using Application.Responses.Permission;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos.PermissionDtos
{
    internal class PermissionMappingProfile : Profile
    {
        public PermissionMappingProfile()
        {

            CreateMap<Permission, GetAllPermissionResponse>()
                    .ForMember(dest => dest.Name , opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
} 

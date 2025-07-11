﻿using Application.Requests.ServiceTab;
using Application.Responses;
using Application.Responses.TabService;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos.TabServiceMap
{
    public class TabServiceMappingProfile : Profile
    {
        public TabServiceMappingProfile() 
        {
            CreateMap<TabServices, BaseAddResponse>();

            CreateMap<IEnumerable<DateOnly>, GetDatesResponse>()
            .ForMember(dest => dest.Dates, opt => opt.MapFrom(src => src));

            CreateMap<AddServiceRequest, TabServices>() 
             .ForMember(dest => dest.ArrivalTimeOfMember, opt => opt.MapFrom(src => src.MemberArrivalTime))
             .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
             .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.EndTime)))
             .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.StartTime)))
             .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Comment));

            CreateMap<TabServices, GetTabServiceListResponse>()
             .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Notes))
             .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
             .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
             .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
             .ForMember(dest => dest.MemberArrivalTime, opt => opt.MapFrom(src => src.ArrivalTimeOfMember));
        }
    }
}

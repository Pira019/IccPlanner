using Application.Requests.Ministry;
using Application.Responses.Ministry;
using AutoMapper; 

namespace Application.Dtos.Ministry
{
    public class MinistryMappingProfile : Profile
    {
        public MinistryMappingProfile()
        {
            CreateMap<AddMinistryRequest, Domain.Entities.Ministry>();
            CreateMap<Domain.Entities.Ministry, AddMinistryResponse>();
        }
         
    }
}

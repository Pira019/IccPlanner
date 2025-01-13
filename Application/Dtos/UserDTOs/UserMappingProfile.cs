

using Application.Requests.User;
using AutoMapper; 

namespace Application.Dtos.UserDTOs
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserRequest, CreateUserDTO>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Member, opt => opt.MapFrom(src =>
                    new Domain.Entities.Member
                    {
                        Name = src.Name,
                        LastName = src.LastName,
                        Sexe = src.Sexe,
                        City = src.City,
                        Quarter = src.Quarter,
                        EntryDate = src.EntryDate,
                        BirthDate = src.BirthDate,  
                    }
                ));  
        }
    }
}

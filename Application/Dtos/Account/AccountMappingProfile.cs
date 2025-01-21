

using Application.Dtos.Account;
using Application.Requests.Account;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos.UserDTOs
{
    /// <summary>
    ///   Permet de mapper tous les chamos qu'on a besoin
    /// </summary>
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<CreateAccountRequest, CreateAccountDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src =>
                    new User
                    {
                        Email = src.Email,
                        PhoneNumber = src.Tel,
                        PasswordHash = src.Password,
                        UserName = src.Email,
                        //Creation du membre
                        Member = new Member
                        {
                            Name = src.Name,
                            LastName = src.LastName,
                            Sexe = src.Sexe,
                            City = src.City,
                            Quarter = src.Quarter
                        }
                    }
                ));
        }
    }
}

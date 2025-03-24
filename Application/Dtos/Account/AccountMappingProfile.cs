

using Application.Dtos.Account;
using Application.Dtos.Department;
using Application.Helper;
using Application.Requests.Account;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos.UserDTOs
{
    /// <summary>
    ///   Permet de mapper tous les champs qu'on a besoin
    /// </summary>
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<CreateAccountRequest, CreateAccountDto>()
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

            CreateMap<ImportMemberDto, User>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Contact))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => Utiles.GeneratedEmail(src.Contact)))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => Utiles.GeneratedEmail(src.Contact)))
                .ForMember(dest => dest.Member, opt => opt.MapFrom(src =>  new Member
                    {
                        Name = src.Nom,
                        Sexe = src.Sex,
                        AddedById = src.AddedBy,
                        DepartmentMembers =  { new DepartmentMember { DepartmentId = src.DepartmentId } }                        
                    }  
                 ))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.MemberId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore());
        }
    }
}

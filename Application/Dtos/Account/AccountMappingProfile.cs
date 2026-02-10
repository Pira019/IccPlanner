

using Application.Dtos.Account;
using Application.Dtos.Department;
using Application.Helper;
using Application.Requests.Account;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Dtos.UserDTOs
{
    /// <summary>
    ///   Permet de mapper tous les champs qu'on a besoin
    /// </summary>
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<CreateAccountDto, DepartmentMember>()
            .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.User!.Member.Id))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom((src, dest, _, context) =>
                (int)context.Items["DepartmentId"])) // tu passes DepartmentId via le contexte
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => MemberStatus.Active))
            .ForMember(dest => dest.DateEntry, opt => opt.MapFrom(_ => DateOnly.FromDateTime(DateTime.UtcNow)))
            .ForMember(dest => dest.NickName, opt => opt.MapFrom(src =>
                 $"{char.ToUpper(src.User!.Member.Name[0])}{src.User.Member.Name.Substring(1).ToLower()} {char.ToUpper(src.User.Member.LastName[0])}."
            ));


            CreateMap<CreateAccountRequest, Member>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Sexe, opt => opt.MapFrom(src => src.Sexe))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Quarter, opt => opt.MapFrom(src => src.Quarter));

            // Pour CreateInvAccountRequest : pas besoin d'IncludeBase, les propriétés publiques sont déjà mappées
            CreateMap<CreateInvAccountRequest, Member>();

            // -------------------------------
            // Mapping pour User
            // -------------------------------
            CreateMap<CreateAccountRequest, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Tel))
                .ForMember(dest => dest.Member, opt => opt.MapFrom(src => src)); // utilise le mapping Member défini ci-dessus

            // Pour CreateInvAccountRequest : réutilise le mapping parent + EmailConfirmed = true
            CreateMap<CreateInvAccountRequest, User>()
                .IncludeBase<CreateAccountRequest, User>()
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true));

            // -------------------------------
            // Mapping pour CreateAccountDto
            // -------------------------------
            CreateMap<CreateAccountRequest, CreateAccountDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));

            CreateMap<CreateInvAccountRequest, CreateAccountDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));



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
                .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore());
        }
    }
}

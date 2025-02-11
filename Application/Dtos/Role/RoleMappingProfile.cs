 using AutoMapper;
using Domain.Entities;

namespace Application.Dtos.Role
{
    /// <summary>
    /// Permet de configurer le mapping de ROLE
    /// </summary>
    public class RoleMappingProfile : Profile
    {
        /// <summary>
        /// Mapping profile
        /// </summary>
        public RoleMappingProfile() 
        {
           CreateMap<Domain.Entities.Role, GetRolesDto>();
        }
    }
}

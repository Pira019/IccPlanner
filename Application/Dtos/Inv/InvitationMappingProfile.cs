using Application.Responses.Invitation;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos.Inv
{
    internal class InvitationMappingProfile :Profile
    {
      public InvitationMappingProfile() {
	   CreateMap<Invitation, InvitationResponse>();
	  }
   }
}

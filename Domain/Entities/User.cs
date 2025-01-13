using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    /// <summary>
    /// Table utilisateur qui permet d'indentifier un utilisateur
    /// </summary>
    public class User : IdentityUser
    {
        public Guid MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public MemberStatusEnum Status { get; set; } = MemberStatusEnum.Active;
        public List<Role> Roles { get; } = [];
    }
}

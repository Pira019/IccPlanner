using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    /// <summary>
    /// Table User qui permet d'indentifier un utilisateur
    /// </summary>
    public class User : IdentityUser
    {
        public Guid MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public MemberStatus Status { get; set; } = MemberStatus.Active;
        public List<Role> Roles { get; } = [];
    }
}

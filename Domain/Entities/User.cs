using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    /// <summary>
    /// Table User qui permet d'indentifier un utilisateur
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        ///     Représente l'identifiant unique du membre associé à l'utilisateur.
        /// </summary>
        public Guid MemberId { get; set; }

        /// <summary>
        ///     Représente le membre associé à l'utilisateur.
        /// </summary>
        public Member Member { get; set; } = null!;
        public MemberStatus Status { get; set; } = MemberStatus.Active;
         
    }
}

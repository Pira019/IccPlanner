using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Table utilisateur qui permet d'indentifier un utilisateur
    /// </summary>
    public class User
    {
        public Guid Id { get; set; } 
        public string Password { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public MemberStatusEnum Status { get; set; } = MemberStatusEnum.Active;
    }
}

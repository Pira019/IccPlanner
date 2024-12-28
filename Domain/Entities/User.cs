using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Table utilisateur qui permet d'indentifier un utilisateur
    /// </summary>
    public class User
    {
        private Guid Id { get; set; } 
        private string password { get; set; }
        private Guid MemberId { get; set; }
        private Member Member { get; set; } = null!;
        private MemberStatusEnum Status { get; set; } = MemberStatusEnum.Active;
    }
}

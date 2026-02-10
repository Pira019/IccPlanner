using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Account
{
    /// <summary>
    ///     Modele de donnée pour créer un compte à partir d'une invitation.
    /// </summary>
    public class CreateInvAccountRequest : CreateAccountRequest
    {  
        public required int InvitationId { get; set; }

        [MaxLength(4)]
        public required string Code { get; set; }
    }
}

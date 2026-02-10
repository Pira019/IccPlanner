using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Invitation
{
    /// <summary>
    ///     Permet d'envoyer une invitation
    ///     Modèle de donnée pour envoyer une invitation.
    /// </summary>
    public class SendRequest
    {
        [MaxLength(55)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        ///     Addresse email de l'invité.
        /// </summary>

        [EmailAddress]
        public string Email { get; set; } = null!;
        /// <summary>
        ///     Message personnalisé pour l'invitation.
        /// </summary>
        public int DepartmentID { get; set; }
    }
}

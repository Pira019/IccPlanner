using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Account
{
    /// <summary>
    /// Model de donnee pour l'authentificaion
    /// </summary>
    public class LoginRequest
    {
        [Required] 
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Indique si l'utilisateur souhaite rester connecté.
        /// </summary>
        public bool Remember { get; set; }
    }
}

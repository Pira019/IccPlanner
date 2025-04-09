using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Requests.Account
{
    /// <summary>
    /// Model de donnée pour l’authentification
    /// </summary>
    public class LoginRequest
    {
        [Required] 
        [EmailAddress]
        [JsonPropertyName("email")]
        public required string Email { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public required string Password { get; set; }

        /// <summary>
        /// Indique si l'utilisateur souhaite rester connecté.
        /// </summary>
        /// 
        [JsonPropertyName("remember")]
        public bool Remember { get; set; }
    }
}

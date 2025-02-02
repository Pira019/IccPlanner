
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Responses.Account
{
    public class LoginAccountResponse
    {
        [JsonPropertyName("accessToken")]
        [Required]
        public string AccessToken { get; set; }
    }
}

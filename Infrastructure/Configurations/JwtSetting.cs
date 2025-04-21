 
namespace Infrastructure.Configurations
{
    /// <summary>
    /// Classe pour bind la configuration Jwt
    /// </summary>
    public class JwtSetting
    {
        public string? Secret { get; set; }

        /// <summary>
        /// Durer d'expiration du jeton
        /// </summary>
        public int? ExpirationInMinutes { get; set; }
        public string? Audiance { get; set; }
        public string? Issuer { get; set; }
    }
}

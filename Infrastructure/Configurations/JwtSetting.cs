 
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

        /// <summary>
        ///     Durer d'expiration du jeton pour se souvenir de moi
        /// </summary>
        public int? RememberExpirationInMinutes { get; set; }
        public string? Audiance { get; set; }
        public string? Issuer { get; set; }
    }
}

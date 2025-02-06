 
namespace Infrastructure.Configurations
{
    /// <summary>
    /// Classe pour bind la configuration Jwt
    /// </summary>
    public class JwtSetting
    {
        public required string Secret { get; set; }
        public int ExpirationInMinutes { get; set; }
        public required string Audiance { get; set; }
        public required string Issuer { get; set; }
    }
}

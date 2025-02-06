using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text; 
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens; 

namespace Infrastructure.Configurations
{
    public class TokenProvider
    {
        private readonly IConfiguration _configuration;

        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
             _appSetting  = _configuration.GetRequiredSection("AppSetting").Get<AppSetting>()!;
        }


        private AppSetting _appSetting;


        public string Create(User user)
        {
            string secrteKey = _appSetting.JwtSetting.Secret;
            var secutityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrteKey));

            var credentials = new SigningCredentials(secutityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(JwtRegisteredClaimNames.Sub , user.Id.ToString())  
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(_appSetting.JwtSetting.ExpirationInMinutes),
                SigningCredentials = credentials,
                Issuer = _appSetting.JwtSetting.Issuer,
                Audience = _appSetting.JwtSetting.Audiance,
            };

            var handler = new Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler();

            return handler.CreateToken(tokenDescriptor);
        }
    }
}

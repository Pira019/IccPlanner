using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Configurations
{
    public class TokenProvider
    {
        private readonly IConfiguration _configuration;
        private AppSetting? _appSetting; 
        
        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _appSetting = _configuration.GetRequiredSection("AppSetting").Get<AppSetting>()!;
        }

        public string Create(User user, ICollection<string> userRolesName)
        {  
            string secrteKey = _appSetting?.JwtSetting?.Secret!;
            var secutityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrteKey));

            var credentials = new SigningCredentials(secutityKey, SecurityAlgorithms.HmacSha256); 

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub , user.Id.ToString()),
            };
            userRolesName.ToList().ForEach(roleName => claims.Add(new Claim(ClaimTypes.Role, roleName)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes((double)_appSetting?.JwtSetting?.ExpirationInMinutes!),
                SigningCredentials = credentials,
                Issuer = _appSetting.JwtSetting.Issuer,
                Audience = _appSetting.JwtSetting.Audiance,
            };

            var handler = new Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler();

            return handler.CreateToken(tokenDescriptor);
        }
    }
}

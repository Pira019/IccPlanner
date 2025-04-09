using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Infrastructure.Configurations.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Configurations
{
    public class TokenProvider : ITokenProvider
    { 
        private readonly AppSetting _appSetting;
        public static readonly string _accessToken = "AccesssToken";

        public TokenProvider(IOptions<AppSetting> options)
        {
             _appSetting = options.Value;
        }

        public string Create(User user, ICollection<string> userRolesName)
        {
            string secrteKey = _appSetting?.JwtSetting?.Secret!;
            var secutityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrteKey));

            var credentials = new SigningCredentials(secutityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub , user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
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

        public void AddJwtToCookie(HttpResponse httpResponse, string token)
        {
            httpResponse.Cookies.Append(_accessToken, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite =  SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddMinutes((double)_appSetting?.JwtSetting?.ExpirationInMinutes!),
            });
        }
         
    }
}

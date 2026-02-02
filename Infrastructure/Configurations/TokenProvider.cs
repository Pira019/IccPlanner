using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Domain.Entities;
using Infrastructure.Configurations.Interface;
using Infrastructure.Security.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Configurations
{
    public class TokenProvider : ITokenProvider
    {
        private readonly AppSetting _appSetting;

        /// <summary>
        /// Nom du token
        /// </summary>
        public const string AccessToken = "accessToken";

        public TokenProvider(IOptions<AppSetting> options)
        {
            _appSetting = options.Value;
        }

        public string Create(string user, ICollection<string> userRolesName, ICollection<string> claimslst, bool rememberMe)
        {
            string secrteKey = _appSetting?.JwtSetting?.Secret!;
            var secutityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrteKey));

            var credentials = new SigningCredentials(secutityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub , user),
                new Claim(ClaimTypes.NameIdentifier , user),
            };

            var allPermissions = new List<string>();
            // Ajouter les permissions simples
          
            claimslst.ToList().ForEach(p => allPermissions.Add(p)); //claims

            claims.Add(new Claim(ClaimTypes.Role, JsonSerializer.Serialize(userRolesName.ToList())));            
            claims.Add(new Claim(ClaimsConstants.PERMISSION, JsonSerializer.Serialize(allPermissions)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes((double)_appSetting?.JwtSetting.ExpirationInMinutes!),
                SigningCredentials = credentials,
                Issuer = _appSetting?.JwtSetting.Issuer,
                Audience = _appSetting?.JwtSetting.Audiance,
            };

            var handler = new Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler();

            return handler.CreateToken(tokenDescriptor);
        }

        public Task AppendUserCookie(string token, HttpResponse httpResponse, bool rememberMe)
        {
            var cookieOption = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                MaxAge = TimeSpan.FromMinutes((GetExpirationInMinutes(rememberMe))),
            };

            httpResponse.Cookies.Append(AccessToken, token, cookieOption);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Obtenir la durée d'expiration du token en minutes
        /// </summary>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        private int GetExpirationInMinutes(bool rememberMe)
        {
            var minutes = rememberMe
                ? _appSetting.JwtSetting.RememberExpirationInMinutes
                : _appSetting.JwtSetting.ExpirationInMinutes;
            return (int)minutes!;
        }
    }

}

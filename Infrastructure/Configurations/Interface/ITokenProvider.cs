using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Configurations.Interface
{
    public interface ITokenProvider
    {
        /// <summary>
        /// Générer le JWT token
        /// </summary>
        /// <param name="user">
        /// Utiliser authentifier <see cref="User"/>
        /// </param>
        /// <param name="userRolesName">
        /// LListe de roles
        /// </param>
        /// <returns>
        ///  Retourner le token 
        /// </returns>
        public string Create(User user, ICollection<string> userRolesName);

        /// <summary>
        /// Stocker le token dans le cookie
        /// </summary>
        /// <param name="token">
        /// JWT token généré
        /// </param>
        /// <returns>
        /// Retourner un <see cref="Task"/>
        /// </returns>
        public Task AppendUserCookie(string token, HttpResponse httpResponse);
    }
}

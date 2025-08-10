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
        /// <param name="rememberMe">
        ///  Indique si l'utilisateur veut rester connecté ou pas
        /// </param>
        /// <returns>
        ///  Retourner le token 
        /// </returns>
        public string Create(User user, ICollection<string> userRolesName, bool rememberMe);

        /// <summary>
        /// Stocker le token dans le cookie
        /// </summary>
        /// <param name="token">
        /// JWT token généré
        /// </param>
        /// <param name="token">
        ///      Indique si l'utilisateur veut rester connecté ou pas
        /// </param>
        /// <returns>
        /// Retourner un <see cref="Task"/>
        /// </returns>
        public Task AppendUserCookie(string token, HttpResponse httpResponse, bool rememberMe);
    }
}

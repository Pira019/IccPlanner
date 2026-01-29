using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Configurations.Interface
{
    public interface ITokenProvider
    {
        /// <summary>
        ///     Permet de créer un token JWT
        /// </summary>
        /// <param name="userId">
        ///     Identifiant de l'utilisateur
        /// </param>
        /// <param name="userRolesName">
        ///     Liste des rôles de l'utilisateur
        /// </param>
        /// <param name="claims">
        ///     Liste des claims(permissions) de l'utilisateur
        /// </param>
        /// <param name="rememberMe">
        ///     Indique si l'utilisateur veut rester connecté ou pas
        /// </param>
        /// <returns>
        ///     Retourner le token JWT sous forme de chaîne de caractères
        /// </returns>
        public string Create(string userId, ICollection<string> userRolesName, ICollection<string> claims, bool rememberMe);

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

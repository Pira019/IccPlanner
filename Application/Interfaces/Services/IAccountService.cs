using Application.Requests.Account;
using Application.Responses.Account;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Services
{
    /// <summary>
    ///   Permet de geérer les actions d'un compte
    /// </summary>
    public interface IAccountService
    {
        public Task<IdentityResult> CreateAccount(CreateAccountRequest request, bool isAdmin = false);

        /// <summary>
        ///     Creer un compte à partir d'une invitation, 
        ///     en utilisant les informations fournies dans la requête <see cref="CreateInvAccountRequest"/>.
        /// </summary>
        /// <param name="request">
        ///     Modele de création de compte d'invitation <see cref="CreateInvAccountRequest"/> 
        ///     contenant les détails nécessaires pour créer le compte.
        /// </param>
        /// <returns></returns>
        public Task<Result<bool>> CreateIntvAccount( CreateInvAccountRequest request);

        /// <summary>
        ///  Confirmer l'adresse Email d'un compte
        /// </summary>
        /// <param name="user">L'utilisateur a valider</param>
        /// <param name="code">Code générer dans l'URL de confirmation</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="IdentityResult"/> de l'opération </returns>
        public Task<IdentityResult> ConfirmEmailAccount(User user, string code);

        /// <summary>
        /// Trouver l'utilisateur par Id
        /// </summary>
        /// <param name="userId">Id de l 'utilisateur</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="User"/> de l'opération </returns>
        public Task<User?> FindUserAccountById(string userId);

        /// <summary>
        ///  Authentifier un utilisateur
        /// </summary>
        /// <param name="loginRequest">
        ///     Modele de connexion <see cref="LoginRequest"/>
        /// </param>
        /// <returns>
        ///     Resultat de l'opération <see cref="Result{LoginAccountResponse}"/>
        /// </returns>
        public Task<Result<LoginAccountResponse>> Login(LoginRequest loginRequest);

        /// <summary>
        /// Récuperer un compte par email
        /// </summary>
        /// <param name="email">Email de l'utilisateur</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="User"/> de l'opération </returns>
        public Task<User?> FindUserAccountByEmail(string email);

        public Task<bool> IsAdminExistsAsync();

        /// <summary>
        /// Recuperer les roles d'un utilsateur
        /// </summary>
        /// <param name="user">Represente un utilisateur</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant les noms de roles </returns>
        public Task<ICollection<string>> GetUserRoles(User user);

        /// <summary>
        ///     Obtient les revendications (claims) de l'utilisateur actuel.
        /// </summary>
        /// <returns>
        ///     Revendications de l'utilisateur sous forme de <see cref="ClaimsResponse"/>.
        /// </returns>
        public ClaimsResponse GetUserClaims();


    } 
}


using System.Threading.Tasks;
using Application.Requests.Account;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Services
{
    /// <summary>
    ///   Permet de gerer les actions d'un compte
    /// </summary>
    public interface IAccountService
    {
        public Task<IdentityResult> CreateAccount(CreateAccountRequest request);

        /// <summary>
        ///  Confirmer l'adresse Email d'un compte
        /// </summary>
        /// <param name="user">L'utilisateur a valider</param>
        /// <param name="code">Code generer dans l'URL de confirmation</param>
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
        /// Authentifier un utilisateur
        /// </summary>
        /// <param name="loginRequest">Model de donnée pour authetification</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="SignInResult"/> de l'opération </returns>
        public Task<SignInResult> Login(LoginRequest loginRequest);
    } 
}

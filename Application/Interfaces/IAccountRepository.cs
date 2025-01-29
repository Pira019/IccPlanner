﻿

using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    /// <summary>
    ///   Cette interface gere les action d'un compte
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Creer un compte
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns>Objet IdentityResult</returns>
        public Task<IdentityResult> CreateAsync(User user, string password);
        public Task<User?> FindByEmailAsync(string email);

        /// <summary>
        /// Trouver un compte(User) par l'id
        /// </summary>
        /// <param name="id">Id de l'utilisateur</param>
        /// <returns></returns>
        public Task<User?> FindByIdAsync(string id);

        /// <summary>
        /// Confirmer l'adresse email d'un compte
        /// </summary>
        /// <param name="user">L'utilisateur a valider</param>
        /// <param name="token">le token de verification</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="IdentityResult"/> de l'opération </returns>
        public Task<IdentityResult> ConfirmAccountEmailAsync(User user, string token);

        /// <summary>
        /// Permet d'authentifier un utilisateur
        /// </summary>
        /// <param name="email">Email ou le username d'un utilisateur </param>
        /// <param name="password">Le mot de passe de l'utilisateur</param>
        /// <param name="isPersistent">Flag indiquant si le cookie de connexion doit persister après la fermeture du navigateur.</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="SignInResult"/> de l'opération </returns>
        public Task<SignInResult> SignIn (string email, string password, bool isPersistent = false);

    }
}

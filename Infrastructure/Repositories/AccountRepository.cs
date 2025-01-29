
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    /// <summary>
    ///   Permet de faire des action dans la DB
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<IdentityResult> ConfirmAccountEmailAsync(User user, string token)
        {
            return _userManager.ConfirmEmailAsync(user, token);
        }

        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        /// <summary>
        /// Trouver un utilisateur par code.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Objet User</returns>
        public Task<User?> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<User?> FindByIdAsync(string id)
        {
            return (_userManager.FindByIdAsync(id));
        }

        public Task<SignInResult> SignIn(string email, string password, bool isPersistent = false)
        {
            return _signInManager.PasswordSignInAsync(email, password, isPersistent, true);
        }
    }
}

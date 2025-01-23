
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
        public AccountRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Creer un compte
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns>Objet IdentityResult</returns>
        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            return _userManager.CreateAsync(user,password);
        }

        /// <summary>
        /// Trouver un utilisateur par email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Objet User</returns>
        public Task<User?> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }
    }
}

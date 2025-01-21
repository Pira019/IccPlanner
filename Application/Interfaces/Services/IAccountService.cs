
using Application.Requests.Account;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Services
{
    /// <summary>
    ///   Permet de gerer les actions d'un compte
    /// </summary>
    public interface IAccountService
    {
        public Task<IdentityResult> CreateAccount(CreateAccountRequest request);
    }
}



using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    /// <summary>
    ///   Cette interface gere les action d'un compte
    /// </summary>
    public interface IAccountRepository
    { 
        public Task<IdentityResult> CreateAsync(User user, string password);
    }
}

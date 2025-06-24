using Application.Helper;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IccPlanner.Controllers
{
    /// <summary>
    ///     Sert de base pour les contrôleurs du planner.
    /// </summary>
    /// 

    [ApiController]
    public class PlannerBaseController : ControllerBase
    { 
        protected readonly IAccountRepository AccountRepository;

        public PlannerBaseController(IAccountRepository accountRepository)
        {
            this.AccountRepository = accountRepository;  
        }

        /// <summary>
        ///     Récupère l'ID d'authentification du membre à partir des revendications de l'utilisateur.
        /// </summary>
        /// <returns>
        ///     La valeur de l'ID d'authentification du membre ou Guid.Empty si aucun membre n'est trouvé.
        /// </returns>
        protected async Task<Guid> GetMemberAuthIdAsync()
        {
            var userId = Utiles.GetUserIdFromClaims(User);
            var member = await AccountRepository.GetAuthMemberAsync(userId);
            return member?.Id ?? Guid.Empty;
        }
    }
}

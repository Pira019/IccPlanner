using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    ///     Interface for Invitation Repository
    /// </summary>
    public interface IInvitationRepository : IBaseRepository<Invitation>
    {
        /// <summary>
        ///     Permet de savoir si un email a déjà été utilisé pour une invitation.
        ///     Note : Une invation est considérée comme utilisé veut dire que l'utilisateur a cree un compte.
        /// </summary>
        /// <param name="email">
        ///     Email à vérifier.
        /// </param>
        /// <returns>
        ///     Retourne true si l'email a été utilisé, false s'il ne l'a pas été, ou null en cas d'erreur.
        /// </returns>
        public Task<bool> IsEmailUsedAsync(string email);

        /// <summary>
        ///     Permet de trouver une invitation par email.
        /// </summary>
        /// <param name="email">
        ///     Email de l'invitation à trouver.
        /// </param>
        /// <returns>
        ///     Retourne l'invitation si trouvée, sinon null.
        /// </returns>
        public Task<Invitation?> FindByEmail(string email);

        /// <summary>
        ///     Marque une invitation comme utilisée. Cela peut être appelé après que l'utilisateur ait créé un compte avec succès en utilisant l'invitation.
        /// </summary>
        /// <returns></returns>
        public Task MarkAsUsedAsync(int invitationId); 

    /// <summary>
    /// 
    /// </summary>
    /// <param name="invitationId"></param>
    /// <returns></returns>
        public Task<Invitation?> FindValidInv(int invitationId); 
    }
}

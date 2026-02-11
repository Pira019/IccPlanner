using Application.Requests.Invitation;
using Application.Responses.Invitation;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    /// <summary>
    ///     Interface for Invitation Service
    /// </summary>
    public interface IInvitationService : IBaseService<Invitation> 
    {
        /// <summary>
        ///     Ends an invitation based on the provided request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<Result<bool>> SendInvitationAnsyc(SendRequest request);

        /// <summary>
        ///   Permet de trouver une invitation valide.
        /// </summary>
        /// <param name="id">
        ///     Invitation ID to search for a valid invitation.
        /// </param>
        /// <returns>
        ///     Returns a Result containing an InvitationResponse if a valid invitation is found, or an error message if not.
        /// </returns>
        public Task<Result<InvitationResponse>> FindValidInviation(int id);
    }
}

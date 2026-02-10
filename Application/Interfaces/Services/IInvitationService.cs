using Application.Requests.Invitation;
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
    }
}

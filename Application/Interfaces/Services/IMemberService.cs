using Application.Requests.Member;
using Application.Responses.Member; 

namespace Application.Interfaces.Services
{
    public interface IMemberService
    {
        public Task<Result<MembersResponse>> GetByDepartmentIdAsync(int departmentId);
        public Task<bool> IsMemberInDepartmentAsync(Guid userId, int departmentId);
        /// <summary>
        ///     Récupère le profil du membre connecté.
        /// </summary>
        public Task<ProfileResponse?> GetProfileAsync(Guid memberId);

        /// <summary>
        ///     Met à jour le profil du membre connecté.
        /// </summary>
        public Task<Result<bool>> UpdateProfileAsync(Guid memberId, UpdateProfileRequest request);
        /// <summary>
        ///     Récupère les anniversaires du mois pour les départements du membre connecté.
        /// </summary>
        public Task<List<BirthdayResponse>> GetBirthdaysAsync(Guid memberId, int month);
    }
}

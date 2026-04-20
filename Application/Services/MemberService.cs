using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Member;
using Application.Responses.Member;
using Shared.Ressources;

namespace Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IDepartmentMemberRepository _departmentMemberRepository;
        private readonly IAccountRepository _accountRepository;

        public MemberService(IMemberRepository memberRepository, IDepartmentMemberRepository departmentMemberRepository, IAccountRepository accountRepository)
        {
            _memberRepository = memberRepository;
            _departmentMemberRepository = departmentMemberRepository;
            _accountRepository = accountRepository;
        }

        public async Task<Result<MembersResponse>> GetByDepartmentIdAsync(int departmentId)
        {
            var members = await _memberRepository.GetByDepartmentIdAsync(departmentId);
            return Result<MembersResponse>.Success(members);
        }

        public async Task<bool> IsMemberInDepartmentAsync(Guid memberId, int departmentId)
        { 
            var departmentMemberId = await _departmentMemberRepository.GetMemberInDepartmentIdAsync(departmentId, memberId);
            return departmentMemberId != null;
        }

        /// <summary>
        ///     Récupère le profil du membre connecté avec ses départements.
        /// </summary>
        public async Task<ProfileResponse?> GetProfileAsync(Guid memberId)
        {
            return await _memberRepository.GetProfileAsync(memberId);
        }

        /// <summary>
        ///     Met à jour le profil du membre connecté.
        /// </summary>
        public async Task<Result<bool>> UpdateProfileAsync(Guid memberId, UpdateProfileRequest request)
        {
            var member = await _memberRepository.GetByGuidAsync(memberId);
            if (member == null)
            {
                return Result<bool>.Fail(ValidationMessages.USER_NOT_FOUND);
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                member.Name = request.Name;
            }
            if (request.LastName != null)
            {
                member.LastName = request.LastName;
            }
            if (!string.IsNullOrWhiteSpace(request.Sexe))
            {
                member.Sexe = request.Sexe;
            }
            if (request.City != null)
            {
                member.City = request.City;
            }
            if (request.Quarter != null)
            {
                member.Quarter = request.Quarter;
            }
            if (request.BirthDate != null)
            {
                // Format attendu : MM-dd (ex: "06-12" pour le 12 juin)
                if (request.BirthDate.Length == 5 && request.BirthDate.Contains('-'))
                {
                    var parts = request.BirthDate.Split('-');
                    if (int.TryParse(parts[0], out var m) && int.TryParse(parts[1], out var d))
                    {
                        member.BirthDate = new DateOnly(2000, m, d);
                    }
                }
            }

            await _memberRepository.UpdateAsync(member);

            return Result<bool>.Success(true);
        }

        /// <summary>
        ///     Récupère les anniversaires du mois pour les départements du membre connecté.
        /// </summary>
        public async Task<List<BirthdayResponse>> GetBirthdaysAsync(Guid memberId, int month)
        {
            // Récupérer les départements du membre
            var profile = await _memberRepository.GetProfileAsync(memberId);
            if (profile == null || !profile.Departments.Any())
            {
                return new List<BirthdayResponse>();
            }

            var departmentIds = profile.Departments.Select(d => d.DepartmentId).ToList();
            return await _memberRepository.GetBirthdaysByMonthAsync(departmentIds, month);
        }
    }
}

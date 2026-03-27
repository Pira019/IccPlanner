using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Responses.Member;

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
    }
}

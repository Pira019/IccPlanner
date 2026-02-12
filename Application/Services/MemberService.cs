using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Responses.Member;

namespace Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Result<MembersResponse>> GetByDepartmentIdAsync(int departmentId)
        {
            var members = await _memberRepository.GetByDepartmentIdAsync(departmentId);
            return Result<MembersResponse>.Success(members);
        }
    }
}

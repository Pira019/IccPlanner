using Application.Responses.Member; 

namespace Application.Interfaces.Services
{
    public interface IMemberService
    {
        public Task<Result<MembersResponse>> GetByDepartmentIdAsync(int departmentId);
        public Task<bool> IsMemberInDepartmentAsync(Guid userId, int departmentId);
    }
}

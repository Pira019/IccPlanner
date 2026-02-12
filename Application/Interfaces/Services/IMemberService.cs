using Application.Responses.Member; 

namespace Application.Interfaces.Services
{
    public interface IMemberService
    {
        public Task<Result<MembersResponse>> GetByDepartmentIdAsync(int departmentId);
    }
}

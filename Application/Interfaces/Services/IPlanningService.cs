using Application.Interfaces.Repositories;
using Application.Requests.Planning;
using Application.Responses.Planning;

namespace Application.Interfaces.Services
{
    public interface IPlanningService
    {
        public Task<Result<AssignMemberResponse>> AssignMemberAsync(AssignMemberRequest request, Guid actionById, int departmentId);
    }
}

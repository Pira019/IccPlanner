using Application.Interfaces.Repositories;
using Application.Requests.Planning;
using Application.Responses.Planning;

namespace Application.Interfaces.Services
{
    public interface IPlanningService
    {
        public Task<Result<AssignMemberResponse>> AssignMemberAsync(AssignMemberRequest request, Guid actionById, int departmentId);
        public Task<Result<bool>> UnassignMemberAsync(int planningId, Guid actionById);
        public Task<Result<bool>> UpdatePlanningAsync(int planningId, UpdatePlanningRequest request, Guid actionById);
        public Task<List<MonthlyPlanningResponse>> GetMonthlyPlanningAsync(int month, int year, int departmentId);
    }
}

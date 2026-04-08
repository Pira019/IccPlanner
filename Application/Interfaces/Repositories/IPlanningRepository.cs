using Application.Dtos.Planning;
using Application.Responses.Planning;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IPlanningRepository : IBaseRepository<Planning>
    {
        public Task<bool> ExistsByAvailabilityIdAsync(int availabilityId);
        public Task<PlanningPeriod> GetOrCreatePeriodAsync(int departmentId, int month, int year);
        public Task<Planning?> GetByIdWithDetailsAsync(int id);
        public Task<AvailabilityDetailDto?> GetByAvailabilityDetailAsync(int availabilityId);
        public Task<List<OverlapConflictDto>> GetOverlappingAssignmentsAsync(Guid memberId, DateOnly date, TimeOnly startTime, TimeOnly endTime);
        public Task<List<MonthlyPlanningResponse>> GetMonthlyPlanningAsync(int month, int year, int departmentId);
    }
}

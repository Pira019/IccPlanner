using Application.Responses.Planning;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IPlanningPeriodRepository : IBaseRepository<PlanningPeriod>
    {
        /// <summary>
        ///     Archive les PlanningPeriods dont le mois est passé.
        /// </summary>
        Task<int> ArchivePastPeriodsAsync();

        Task<PlanningPeriod?> GetByDepartmentMonthYearAsync(int departmentId, int month, int year);
        Task DeletePublishedPlanningsAsync(int periodId);
        Task AddPublishedPlanningsAsync(List<PublishedPlanning> plannings);
        Task<List<MyPlanningResponse>> GetMyPlanningAsync(Guid memberId, int month, int year, int? departmentId);
        Task<List<TeamPlanningResponse>> GetTeamPlanningAsync(int departmentId, int month, int year);
    }
}

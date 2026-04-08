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
    }
}

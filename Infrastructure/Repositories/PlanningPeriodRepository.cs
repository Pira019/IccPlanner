using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PlanningPeriodRepository : BaseRepository<PlanningPeriod>, IPlanningPeriodRepository
    {
        public PlanningPeriodRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<int> ArchivePastPeriodsAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var periodsToArchive = await _dbSet
                .Where(pp => !pp.IndArchived)
                .ToListAsync();

            var archived = 0;

            foreach (var period in periodsToArchive)
            {
                var lastDayOfMonth = new DateOnly(period.Year, period.Month, DateTime.DaysInMonth(period.Year, period.Month));

                if (lastDayOfMonth < today)
                {
                    period.IndArchived = true;
                    archived++;
                }
            }

            if (archived > 0)
            {
                await PlannerContext.SaveChangesAsync();
            }

            return archived;
        }

        public async Task<PlanningPeriod?> GetByDepartmentMonthYearAsync(int departmentId, int month, int year)
        {
            return await _dbSet
                .FirstOrDefaultAsync(pp => pp.DepartmentId == departmentId
                    && pp.Month == month
                    && pp.Year == year);
        }

        public async Task DeletePublishedPlanningsAsync(int periodId)
        {
            var existing = await PlannerContext.PublishedPlannings
                .Where(pp => pp.PlanningPeriodId == periodId)
                .ToListAsync();

            if (existing.Any())
            {
                PlannerContext.PublishedPlannings.RemoveRange(existing);
                await PlannerContext.SaveChangesAsync();
            }
        }

        public async Task AddPublishedPlanningsAsync(List<PublishedPlanning> plannings)
        {
            if (plannings.Any())
            {
                await PlannerContext.PublishedPlannings.AddRangeAsync(plannings);
                await PlannerContext.SaveChangesAsync();
            }
        }
    }
}

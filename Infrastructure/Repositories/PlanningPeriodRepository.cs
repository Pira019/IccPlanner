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

        /// <summary>
        ///     Archive les PlanningPeriods dont le mois est passé.
        /// </summary>
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
    }
}

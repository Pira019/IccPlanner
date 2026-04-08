using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class PlanningArchiveService : IPlanningArchiveService
    {
        private readonly IccPlannerContext _context;

        public PlanningArchiveService(IccPlannerContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Archive les PlanningPeriods dont le mois est passé.
        ///     Un mois est considéré passé si le dernier jour du mois est avant la date du jour.
        /// </summary>
        public async Task<int> ArchivePastPeriodsAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            // Récupérer les périodes non archivées dont le mois est terminé
            var periodsToArchive = await _context.PlanningPeriods
                .Where(pp => !pp.IndArchived)
                .ToListAsync();

            var archived = 0;

            foreach (var period in periodsToArchive)
            {
                // Dernier jour du mois de la période
                var lastDayOfMonth = new DateOnly(period.Year, period.Month, DateTime.DaysInMonth(period.Year, period.Month));

                if (lastDayOfMonth < today)
                {
                    period.IndArchived = true;
                    archived++;
                }
            }

            if (archived > 0)
            {
                await _context.SaveChangesAsync();
            }

            return archived;
        }
    }
}

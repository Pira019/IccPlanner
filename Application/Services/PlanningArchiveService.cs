using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class PlanningArchiveService : IPlanningArchiveService
    {
        private readonly IPlanningPeriodRepository _planningPeriodRepository;

        public PlanningArchiveService(IPlanningPeriodRepository planningPeriodRepository)
        {
            _planningPeriodRepository = planningPeriodRepository;
        }

        /// <summary>
        ///     Archive les PlanningPeriods dont le mois est passé.
        ///     Un mois est considéré passé si le dernier jour du mois est avant la date du jour.
        /// </summary>
        public async Task<int> ArchivePastPeriodsAsync()
        {
            return await _planningPeriodRepository.ArchivePastPeriodsAsync();
        }
    }
}

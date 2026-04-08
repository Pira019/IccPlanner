using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IPlanningPeriodRepository : IBaseRepository<PlanningPeriod>
    {
        /// <summary>
        ///     Archive les PlanningPeriods dont le mois est passé.
        ///     Retourne le nombre de périodes archivées.
        /// </summary>
        Task<int> ArchivePastPeriodsAsync();
    }
}

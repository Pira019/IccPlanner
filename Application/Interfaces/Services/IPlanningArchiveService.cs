namespace Application.Interfaces.Services
{
    public interface IPlanningArchiveService
    {
        /// <summary>
        ///     Archive les PlanningPeriods dont le mois est passé.
        ///     Retourne le nombre de périodes archivées.
        /// </summary>
        Task<int> ArchivePastPeriodsAsync();
    }
}

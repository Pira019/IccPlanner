using Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Jobs
{
    /// <summary>
    ///     Job background qui archive automatiquement les PlanningPeriods
    ///     dont le mois est passé. Tourne une fois par jour.
    /// </summary>
    public class PlanningArchiveJob : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PlanningArchiveJob> _logger;

        public PlanningArchiveJob(
            IServiceScopeFactory scopeFactory,
            ILogger<PlanningArchiveJob> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var archiveService = scope.ServiceProvider.GetRequiredService<IPlanningArchiveService>();

                    var totalArchived = await archiveService.ArchivePastPeriodsAsync();

                    if (totalArchived > 0)
                    {
                        _logger.LogInformation("Job PlanningArchive : {Count} période(s) archivée(s).", totalArchived);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erreur lors de l'archivage des plannings.");
                }

                // Attendre 24h avant la prochaine exécution
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}

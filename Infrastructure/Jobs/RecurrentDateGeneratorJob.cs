using Application.Interfaces.Services;
using Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Jobs
{
    /// <summary>
    ///     Job background qui génère les dates récurrentes des programmes.
    ///     Tourne une fois par jour.
    /// </summary>
    public class RecurrentDateGeneratorJob : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RecurrentDateGeneratorJob> _logger;
        private readonly int _defaultDaysAhead;

        public RecurrentDateGeneratorJob(
            IServiceScopeFactory scopeFactory,
            ILogger<RecurrentDateGeneratorJob> logger,
            IOptions<AppSetting> appSettings)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _defaultDaysAhead = appSettings.Value.Parametres?.RecurrentDaysAhead ?? 30;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var recurrentDateService = scope.ServiceProvider.GetRequiredService<IRecurrentDateService>();

                    var totalCreated = await recurrentDateService.GenerateRecurrentDatesAsync(_defaultDaysAhead);

                    if (totalCreated > 0)
                    {
                        _logger.LogInformation("Job RecurrentDateGenerator : {Count} dates créées.", totalCreated);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erreur lors de la génération des dates récurrentes.");
                }

                // Attendre 24h avant la prochaine exécution
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}

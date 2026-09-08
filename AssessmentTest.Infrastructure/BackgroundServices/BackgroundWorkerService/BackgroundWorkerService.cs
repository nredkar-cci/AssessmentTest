using AssessmentTest.Infrastructure.BackgroundServices.EmailJob;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AssessmentTest.Infrastructure.BackgroundServices.BackgroundWorkerService
{
    public class BackgroundWorkerService : BackgroundService
    {
        private static readonly TimeSpan Interval = TimeSpan.FromMinutes(1);

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BackgroundWorkerService> _logger;

        public BackgroundWorkerService(IServiceScopeFactory scopeFactory, ILogger<BackgroundWorkerService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(Interval);

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    await DoWorkAsync(stoppingToken);
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "Background work failed.");
                }
            }
        }

        private async Task DoWorkAsync(CancellationToken ct)
        {

            using var scope = _scopeFactory.CreateScope();
            var pendingEmailJob = scope.ServiceProvider.GetRequiredService<PendingEmailJob>();

             await pendingEmailJob.SendPendingEmailsAsync(ct);
            _logger.LogDebug("Background worker tick at {Timestamp:o}.", DateTime.UtcNow);

            await Task.CompletedTask;
        }
    }
}

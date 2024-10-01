using Core.Services;

namespace ServiceMonitoring;

public class MonitoringWorker(ILogger<MonitoringWorker> logger)
    : WorkerBase<MonitoringWorker>(logger)
{
    protected override async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("SecurityWorker is running.");

            await Task.Delay(1000, stoppingToken); // Simulate some work
        }
    }
}
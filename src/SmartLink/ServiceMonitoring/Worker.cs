using Core.Services;

namespace ServiceMonitoring;

public class MonitoringWorker : WorkerBase, IHostedService
{
    public MonitoringWorker(ILogger<MonitoringWorker> logger) 
        : base(logger) 
    {
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("MonitoringWorker starting...");
        StartService();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("MonitoringWorker stopping...");
        StopService();
        return Task.CompletedTask;
    }
}
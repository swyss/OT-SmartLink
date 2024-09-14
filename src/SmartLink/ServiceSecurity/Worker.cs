using Core.Services;

namespace ServiceSecurity;

public class SecurityWorker : WorkerBase, IHostedService
{
    public SecurityWorker(ILogger<SecurityWorker> logger) 
        : base(logger) 
    {
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("SecurityWorker starting...");
        StartService();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("SecurityWorker stopping...");
        StopService();
        return Task.CompletedTask;
    }
}
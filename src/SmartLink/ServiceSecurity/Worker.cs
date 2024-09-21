using Core.Logging;
using Core.Repositories;
using Core.Services;
using Microsoft.Extensions.Logging;

namespace ServiceSecurity;

public class SecurityWorker : WorkerBase<SecurityWorker>
{
    public SecurityWorker(ILogger<SecurityWorker> logger, IServiceConfigRepository configRepository)
        : base(logger, configRepository)
    {
    }

    protected override async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("SecurityWorker is running.");

            await Task.Delay(1000, stoppingToken); // Simulate some work
        }
    }
}
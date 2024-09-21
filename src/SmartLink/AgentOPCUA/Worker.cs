using Core.Logging;
using Core.Repositories;
using Core.Services;

namespace AgentOPCUA;

public class OpcuaWorker(ILogger<OpcuaWorker> logger, IServiceConfigRepository configRepository)
    : WorkerBase<OpcuaWorker>(logger, configRepository)
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
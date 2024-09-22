using Core.Services;

namespace AgentOPCUA;

public class OpcuaWorker(ILogger<OpcuaWorker> logger)
    : WorkerBase<OpcuaWorker>(logger)
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
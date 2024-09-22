using Core.Services;

namespace AgentModbus;

public class ModbusWorker : WorkerBase<ModbusWorker>
{
    public ModbusWorker(ILogger<ModbusWorker> logger)
        : base(logger)
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
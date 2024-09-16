using Core.Logging;
using Core.Repositories;
using Core.Services;

namespace AgentModbus;

public class ModbusWorker : WorkerBase
{
    public ModbusWorker(ILogger<ModbusWorker> logger, ServiceConfigRepository configRepository,
        InfluxDBLogger influxLogger)
        : base(logger, configRepository, influxLogger)
    {
    }

    protected override async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("ModbusWorker is running.");
            await Task.Delay(1000, stoppingToken); // Simulate work
        }
    }
}
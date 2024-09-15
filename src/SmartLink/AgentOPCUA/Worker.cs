using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Core.Logging;
using Core.Repositories;
using Core.Services;

namespace AgentOPCUA;

public class OPCUAWorker : WorkerBase
{
    public OPCUAWorker(ILogger<OPCUAWorker> logger, ServiceConfigRepository configRepository,
        InfluxDBLogger influxLogger)
        : base(logger, configRepository, influxLogger)
    {
    }

    protected override async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("OPCUAWorker is running.");

            // Example: Log status or perform some periodic work
            await LogServiceStatusToInfluxDB("Running");

            await Task.Delay(1000, stoppingToken); // Simulate some work
        }
    }
}
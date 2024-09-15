using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Core.Logging;
using Core.Repositories;
using Core.Services;

namespace ServiceMonitoring;

public class MonitoringWorker : WorkerBase
{
    public MonitoringWorker(ILogger<MonitoringWorker> logger, ServiceConfigRepository configRepository,
        InfluxDBLogger influxLogger)
        : base(logger, configRepository, influxLogger)
    {
    }

    protected override async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("MonitoringWorker is running.");

            // Example: Log status or perform some periodic work
            await LogServiceStatusToInfluxDB("Running");

            await Task.Delay(1000, stoppingToken); // Simulate some work
        }
    }
}
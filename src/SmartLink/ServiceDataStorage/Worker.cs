using Core.Logging;
using Core.Repositories;
using Core.Services;

namespace ServiceDataStorage;

public class DataStorageWorker : WorkerBase
{
    public DataStorageWorker(ILogger<DataStorageWorker> logger, ServiceConfigRepository configRepository,
        InfluxDBLogger influxLogger)
        : base(logger, configRepository, influxLogger)
    {
    }

    protected override async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("DataStorageWorker is running.");

            // Example: Log status or perform some periodic work
            await LogServiceStatusToInfluxDB("Running");

            await Task.Delay(1000, stoppingToken); // Simulate some work
        }
    }
}
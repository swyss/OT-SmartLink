using Core.Logging;
using Core.Repositories;
using Core.Services;

namespace ServiceDataStorage;

public class DataStorageWorker :  WorkerBase<DataStorageWorker>
{
    public DataStorageWorker(ILogger<DataStorageWorker> logger, IServiceConfigRepository configRepository)
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
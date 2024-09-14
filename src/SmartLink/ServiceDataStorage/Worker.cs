using Core.Services;

namespace ServiceDataStorage;

public class DataStorageWorker : WorkerBase, IHostedService
{
    public DataStorageWorker(ILogger<DataStorageWorker> logger) 
        : base(logger) 
    {
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("DataStorageWorker starting...");
        StartService(); 
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("DataStorageWorker stopping...");
        StopService();
        return Task.CompletedTask;
    }
}
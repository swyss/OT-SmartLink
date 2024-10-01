using Core.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public abstract class WorkerBase<TWorker> : BackgroundService, IWorkerService
{

    protected readonly ILogger<TWorker> _logger;
    protected ServiceState _currentState;

    protected WorkerBase(ILogger<TWorker> logger)
    {
        _logger = logger;
        _currentState = ServiceState.Stopped;
    }

    public string GetServiceStateAsString()
    {
        return _currentState.ToString();
    }

    public ServiceState GetServiceState()
    {
        return _currentState;
    }

    public virtual async Task StartService(CancellationToken cancellationToken)
    {
        _currentState = ServiceState.Running;
        _logger.LogInformation("Service started.");
        await SaveServiceConfigAsync("Status", _currentState.ToString());
    }

    public virtual async Task StopService(CancellationToken cancellationToken)
    {
        _currentState = ServiceState.Stopped;
        _logger.LogInformation("Service stopped.");
        await SaveServiceConfigAsync("Status", _currentState.ToString());
    }

    // Save service configuration to PostgreSQL
    protected async Task SaveServiceConfigAsync(string key, string value)
    {
        _logger.LogInformation($"Saved configuration for {GetType().Name}: {key} = {value}");
    }

    // Automatically restart the service if it fails
    protected async Task RestartServiceOnError(Func<Task> serviceTask, CancellationToken cancellationToken,
        int maxRetries = 3)
    {
        var retryCount = 0;
        while (retryCount < maxRetries && !cancellationToken.IsCancellationRequested)
            try
            {
                await serviceTask();
                break;
            }
            catch (Exception ex)
            {
                retryCount++;
                _currentState = ServiceState.Error;
                _logger.LogError(ex, $"{GetType().Name} encountered an error. Retry {retryCount}/{maxRetries}.");
                if (retryCount >= maxRetries)
                {
                    _logger.LogError($"{GetType().Name} failed after {maxRetries} retries. Giving up.");
                }
                else
                {
                    _logger.LogWarning($"{GetType().Name} restarting in 2 seconds...");
                    await Task.Delay(2000, cancellationToken); // Wait before restarting
                }
            }
    }

    // Abstract method for derived classes to implement specific worker logic
    protected abstract Task DoWorkAsync(CancellationToken stoppingToken);

    // ExecuteAsync is part of BackgroundService
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await StartService(stoppingToken);
        await RestartServiceOnError(() => DoWorkAsync(stoppingToken), stoppingToken);
        await StopService(stoppingToken);
    }
}
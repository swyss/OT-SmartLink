using Core.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public abstract class WorkerBase : IWorkerService
{
    protected readonly ILogger<WorkerBase> _logger; // Protected field to allow derived classes to access it
    protected ServiceState _currentState;

    protected WorkerBase(ILogger<WorkerBase> logger)
    {
        _logger = logger; // Initialize logger
        _currentState = ServiceState.Stopped; // Initial state is Stopped
    }

    public string GetServiceStateAsString()
    {
        return _currentState.ToString(); // Return current state as a string
    }

    public ServiceState GetServiceState()
    {
        return _currentState;
    }

    public virtual void StartService()
    {
        _currentState = ServiceState.Running; // Set state to Running
        _logger.LogInformation("Service started."); // Log start
    }

    public virtual void StopService()
    {
        _currentState = ServiceState.Stopped; // Set state to Stopped
        _logger.LogInformation("Service stopped."); // Log stop
    }
}
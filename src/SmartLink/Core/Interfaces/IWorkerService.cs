namespace Core.Interfaces;

public interface IWorkerService
{
    string GetServiceStateAsString();
    ServiceState GetServiceState();
    Task StartService(CancellationToken cancellationToken);
    Task StopService(CancellationToken cancellationToken);
}

public enum ServiceState
{
    Stopped,
    Running,
    Error
}
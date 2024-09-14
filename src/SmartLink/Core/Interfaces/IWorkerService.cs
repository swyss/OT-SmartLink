namespace Core.Interfaces;

public interface IWorkerService
{
    string GetServiceStateAsString();
    ServiceState GetServiceState();
    void StartService();
    void StopService();
}

public enum ServiceState
{
    Stopped,
    Running,
    Error
}
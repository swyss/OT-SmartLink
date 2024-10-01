namespace Core.Orchestrator;

public interface IOrchestrator
{
    Task StartAsync();
    Task StopAsync();
    Task RestartAsync();
}
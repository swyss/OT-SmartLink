using Core.Broker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Orchestrator;

public class OrchestratorService : IOrchestrator
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrchestratorService> _logger;

    public OrchestratorService(IServiceProvider serviceProvider, ILogger<OrchestratorService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync()
    {
        _logger.LogInformation("Starting all services and agents...");

        // Start all necessary services, e.g. Modbus, MQTT, etc.
        var serviceBroker = _serviceProvider.GetRequiredService<ServiceBroker>();
        await serviceBroker.StartAllServicesAsync();

        _logger.LogInformation("All services and agents started successfully.");
    }

    public Task StopAsync()
    {
        _logger.LogInformation("Stopping all services and agents...");
        // Logic to stop all services
        return Task.CompletedTask;
    }

    public Task RestartAsync()
    {
        _logger.LogInformation("Restarting all services and agents...");
        // Logic to restart all services
        return Task.CompletedTask;
    }
}
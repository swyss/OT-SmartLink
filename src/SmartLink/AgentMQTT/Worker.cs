using Core.Services;

namespace AgentMQTT;

public class MQTTWorker : WorkerBase, IHostedService
{
    // Constructor with only ILogger as a parameter
    public MQTTWorker(ILogger<MQTTWorker> logger) 
        : base(logger) // Pass the logger to the base class (WorkerBase)
    {
    }

    // StartAsync implementation for IHostedService
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("MQTTWorker starting..."); // Use the protected _logger field
        StartService(); // Call the base class StartService method

        // Add MQTT-specific startup logic here

        return Task.CompletedTask;
    }

    // StopAsync implementation for IHostedService
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("MQTTWorker stopping..."); // Use the protected _logger field
        StopService(); // Call the base class StopService method

        // Add MQTT-specific shutdown logic here

        return Task.CompletedTask;
    }
}
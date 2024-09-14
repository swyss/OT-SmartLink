using Core.Services;

namespace AgentOPCUA;

public class OPCUAWorker : WorkerBase, IHostedService
{
    // Constructor with only ILogger as a parameter
    public OPCUAWorker(ILogger<OPCUAWorker> logger) 
        : base(logger) // Pass the logger to the base class (WorkerBase)
    {
    }

    // StartAsync implementation for IHostedService
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("OPCUAWorker starting..."); // Use the protected _logger field
        StartService(); // Call the base class StartService method

        // Add OPCUA-specific startup logic here

        return Task.CompletedTask;
    }

    // StopAsync implementation for IHostedService
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("OPCUAWorker stopping..."); // Use the protected _logger field
        StopService(); // Call the base class StopService method

        // Add OPCUA-specific shutdown logic here

        return Task.CompletedTask;
    }
}
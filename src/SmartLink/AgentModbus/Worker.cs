using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Core.Services;

namespace AgentModbus;
public class ModbusWorker : WorkerBase, IHostedService // Ensure ModbusWorker implements IHostedService
{
    public ModbusWorker(ILogger<ModbusWorker> logger) 
        : base(logger)
    {
    }

    // StartAsync is required for IHostedService
    public Task StartAsync(CancellationToken cancellationToken)
    {
        StartService(); // Call the base class StartService method
        return Task.CompletedTask;
    }

    // StopAsync is required for IHostedService
    public Task StopAsync(CancellationToken cancellationToken)
    {
        StopService(); // Call the base class StopService method
        return Task.CompletedTask;
    }
}
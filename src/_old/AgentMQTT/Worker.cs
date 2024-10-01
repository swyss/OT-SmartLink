using Core.Services;

namespace AgentMQTT;

public class MqttWorker(ILogger<MqttWorker> logger)
    : WorkerBase<MqttWorker>(logger)
{
    protected override async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("SecurityWorker is running.");

            await Task.Delay(1000, stoppingToken); // Simulate some work
        }
    }
}
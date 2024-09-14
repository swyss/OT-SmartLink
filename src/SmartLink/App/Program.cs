using AgentModbus;
using AgentMQTT;
using AgentOPCUA;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceDataStorage;
using ServiceMonitoring;
using ServiceSecurity;

namespace App;

public static class Program
{
    public static void Main(string[] args)
    {
        // Start both worker services and Avalonia UI
        var host = CreateHostBuilder(args).Build();

        // Start the Avalonia UI and the worker services
        var applicationStarter = new ApplicationStarter();
        applicationStarter.Start();

        // Run the host (which includes background worker services)
        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ModbusWorker>(); // Register worker services
                services.AddHostedService<MQTTWorker>();
                services.AddHostedService<OPCUAWorker>();
                services.AddHostedService<DataStorageWorker>();
                services.AddHostedService<MonitoringWorker>();
                services.AddHostedService<SecurityWorker>();
            });
}
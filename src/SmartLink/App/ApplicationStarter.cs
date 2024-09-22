using AgentModbus;
using AgentMQTT;
using AgentOPCUA;
using Core.Broker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceDataStorage;
using ServiceMonitoring;
using ServiceSecurity;
using System;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace App;

public class ApplicationStarter
{
    private readonly ILogger<ApplicationStarter> _logger = null!;
    private readonly ServiceBroker _serviceBroker = null!;

    public ApplicationStarter(
        ILogger<ApplicationStarter> logger,
        ServiceBroker serviceBroker)
    {
        _logger = logger;
        _serviceBroker = serviceBroker;
    }

    public ApplicationStarter()
    {
    } // Default constructor for Program.Main()

    // Create and configure the host
    public IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) => { config.AddJsonFile("appsettings.json", false, true); })
            .ConfigureServices((hostContext, services) =>
            {
                // Register worker services
                services.AddHostedService<ModbusWorker>();
                services.AddHostedService<MqttWorker>();
                services.AddHostedService<OpcuaWorker>();
                services.AddHostedService<DataStorageWorker>();
                services.AddHostedService<MonitoringWorker>();
                services.AddHostedService<SecurityWorker>();

                // Register dependencies for ServiceBroker
                services.AddSingleton<IWorkerService, ModbusWorker>();
                services.AddSingleton<IWorkerService, MqttWorker>();
                services.AddSingleton<IWorkerService, OpcuaWorker>();
                services.AddSingleton<IWorkerService, DataStorageWorker>();
                services.AddSingleton<IWorkerService, MonitoringWorker>();
                services.AddSingleton<IWorkerService, SecurityWorker>();

                // Register ServiceBroker and other services
                services.AddSingleton<ServiceBroker>();
                services.AddSingleton<ApplicationStarter>();
                
                // Configure appsettings.json configuration
                services.AddOptions();
            });
    }

    public async Task StartAsync(IHost host)
    {
        // Start Avalonia UI in a separate Task and wait for it to signal readiness
        var uiTask = Task.Run(() =>
        {
            try
            {
                AnsiConsole.Markup("[bold cyan]Starting Avalonia UI...[/]\n");
                MainUI.Program.Main(new string[0]); // Start Avalonia UI
            }
            catch (Exception ex)
            {
                _logger?.LogError($"Error starting Avalonia UI: {ex.Message}");
            }
        });

        // Wait for the Avalonia UI to signal readiness
        await MainUI.Program.UiLoaded.Task;
        AnsiConsole.Markup("[bold green]✔ Avalonia UI has started successfully![/]\n");

        // Start all worker services after UI has loaded
        AnsiConsole.Markup("[bold cyan]Starting worker services...[/]\n");
        var serviceBroker = host.Services.GetRequiredService<ServiceBroker>();
        await serviceBroker.StartAllServicesAsync();
        AnsiConsole.Markup("[bold green]✔ All worker services started successfully.[/]\n");
    }
}
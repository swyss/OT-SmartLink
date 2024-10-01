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
using System.Diagnostics;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Orchestrator;
using Core.Services;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace App
{
    public class ApplicationStarter
    {
        public IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Load appsettings.json
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    // Bind appsettings.json to strongly typed classes for modules
                    services.Configure<ModuleSettings>(context.Configuration.GetSection("Modules"));
                    services.Configure<UISettings>(context.Configuration.GetSection("UI"));
                    services.Configure<StartupSettings>(context.Configuration.GetSection("StartupSettings"));
                    services.Configure<OrchestrationSettings>(context.Configuration.GetSection("Orchestration"));

                    // Register all hosted services (Agents and Worker Services) as per appsettings
                    var modulesConfig = context.Configuration.GetSection("Modules").Get<ModuleSettings>();

                    Debug.Assert(modulesConfig != null, nameof(modulesConfig) + " != null");
                    if (modulesConfig.Agents.Exists(a => a.Name == "ModbusAgent" && a.Enabled))
                        services.AddHostedService<ModbusWorker>();

                    if (modulesConfig.Agents.Exists(a => a.Name == "MqttAgent" && a.Enabled))
                        services.AddHostedService<MqttWorker>();

                    if (modulesConfig.Agents.Exists(a => a.Name == "OpcuaAgent" && a.Enabled))
                        services.AddHostedService<OpcuaWorker>();

                    if (modulesConfig.Services.Exists(s => s.Name == "DataStorageService" && s.Enabled))
                        services.AddHostedService<DataStorageWorker>();

                    if (modulesConfig.Services.Exists(s => s.Name == "MonitoringService" && s.Enabled))
                        services.AddHostedService<MonitoringWorker>();

                    if (modulesConfig.Services.Exists(s => s.Name == "SecurityService" && s.Enabled))
                        services.AddHostedService<SecurityWorker>();

                    // Register Core components (Broker, Orchestrator, etc.)
                    var coreConfig = context.Configuration.GetSection("Modules:Core").Get<CoreSettings>();

                    Debug.Assert(coreConfig != null, nameof(coreConfig) + " != null");
                    if (coreConfig.Enabled)
                    {
                        if (coreConfig.Broker.Enabled)
                            services.AddSingleton<ServiceBroker>();

                        if (coreConfig.Orchestrator.Enabled)
                            services.AddSingleton<IOrchestrator, OrchestratorService>();
                    }

                    // Register additional services for UI handling
                    var uiConfig = context.Configuration.GetSection("UI").Get<UISettings>();

                    if (uiConfig is { AvaloniaUI.Enabled: true })
                    {
                        services.AddSingleton<ConsoleHandler>(); // For console interaction
                    }

                    // Configure appsettings.json and other options
                    services.AddOptions();
                });

        public async Task StartAsync(IHost host)
        {
            // Start the console UI and all worker services
            AnsiConsole.Markup("[bold cyan]Starting UI components...[/]\\n");

            var uiSettings = host.Services.GetRequiredService<IConfiguration>().GetSection("UI").Get<UISettings>();

            Debug.Assert(uiSettings != null, nameof(uiSettings) + " != null");
            if (uiSettings.AvaloniaUI.Enabled)
            {
                AnsiConsole.Markup("[bold cyan]Starting Avalonia UI...[/]\\n");
                // Starting Avalonia UI
                var uiTask = Task.Run(() =>
                {
                    try
                    {
                        MainUI.Program.Main(new string[0]); 
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.Markup($"[bold red]Error starting Avalonia UI: {ex.Message}[/]\\n");
                    }
                });

                // Wait for the UI to signal readiness
                await MainUI.Program.UiLoaded.Task;
                AnsiConsole.Markup("[bold green]✔ Avalonia UI has started successfully![/]\\n");
            }

            // Start the orchestrator for services
            AnsiConsole.Markup("[bold cyan]Starting services...[/]\\n");
            var orchestrator = host.Services.GetRequiredService<IOrchestrator>();
            await orchestrator.StartAsync();

            AnsiConsole.Markup("[bold green]✔ All services started successfully![/]\\n");
        }
    }
}

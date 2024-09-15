using AgentModbus;
using AgentMQTT;
using AgentOPCUA;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Core.Broker;
using MainUI.ViewModels;
using MainUI.Views;
using Microsoft.Extensions.Logging;
using ServiceDataStorage;
using ServiceMonitoring;
using ServiceSecurity;
using Core.Repositories;
using Core.Logging;

namespace MainUI;

public partial class App : Application
{
    public static ServiceBroker ServiceBrokerInstance { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            // Initialize ServiceConfigRepository and InfluxDBLogger
            var configRepository =
                new ServiceConfigRepository(
                    "Host=localhost;Database=service_config;Username=postgres;Password=yourpassword");
            var influxLogger =
                new InfluxDBLogger("http://localhost:8086", "your-influxdb-token", "my-bucket", "my-org");

            // Create ServiceBroker with all worker services and necessary dependencies
            ServiceBrokerInstance = new ServiceBroker(
                new ModbusWorker(loggerFactory.CreateLogger<ModbusWorker>(), configRepository, influxLogger),
                new MQTTWorker(loggerFactory.CreateLogger<MQTTWorker>(), configRepository, influxLogger),
                new OPCUAWorker(loggerFactory.CreateLogger<OPCUAWorker>(), configRepository, influxLogger),
                new DataStorageWorker(loggerFactory.CreateLogger<DataStorageWorker>(), configRepository, influxLogger),
                new MonitoringWorker(loggerFactory.CreateLogger<MonitoringWorker>(), configRepository, influxLogger),
                new SecurityWorker(loggerFactory.CreateLogger<SecurityWorker>(), configRepository, influxLogger)
            );

            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(ServiceBrokerInstance)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
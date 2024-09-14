using AgentModbus;
using AgentMQTT;
using AgentOPCUA;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

namespace MainUI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            // Example ServiceBroker setup (adapt as needed)
            var serviceBroker = new ServiceBroker(
                new ModbusWorker(loggerFactory.CreateLogger<ModbusWorker>()),
                new MQTTWorker(loggerFactory.CreateLogger<MQTTWorker>()),
                new OPCUAWorker(loggerFactory.CreateLogger<OPCUAWorker>()),
                new DataStorageWorker(loggerFactory.CreateLogger<DataStorageWorker>()),
                new MonitoringWorker(loggerFactory.CreateLogger<MonitoringWorker>()),
                new SecurityWorker(loggerFactory.CreateLogger<SecurityWorker>())
            );

            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(serviceBroker)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
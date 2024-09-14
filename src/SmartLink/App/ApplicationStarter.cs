using AgentModbus;
using AgentMQTT;
using AgentOPCUA;
using Avalonia;
using Avalonia.Controls;
using Core.Broker;
using Microsoft.Extensions.Logging;
using ServiceDataStorage;
using ServiceMonitoring;
using ServiceSecurity;

namespace App;

public class ApplicationStarter
{
    public void Start()
    {
        // Logger factory setup
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        // Create worker services
        var modbusWorker = new ModbusWorker(loggerFactory.CreateLogger<ModbusWorker>());
        var mqttWorker = new MQTTWorker(loggerFactory.CreateLogger<MQTTWorker>());
        var opcuaWorker = new OPCUAWorker(loggerFactory.CreateLogger<OPCUAWorker>());
        var dataStorageWorker = new DataStorageWorker(loggerFactory.CreateLogger<DataStorageWorker>());
        var monitoringWorker = new MonitoringWorker(loggerFactory.CreateLogger<MonitoringWorker>());
        var securityWorker = new SecurityWorker(loggerFactory.CreateLogger<SecurityWorker>());

        // Create ServiceBroker and pass the workers
        var serviceBroker = new ServiceBroker(
            modbusWorker, 
            mqttWorker, 
            opcuaWorker, 
            dataStorageWorker, 
            monitoringWorker, 
            securityWorker
        );

        // Start all services
        serviceBroker.StartAllServices();

        // Start Avalonia UI
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(null, ShutdownMode.OnMainWindowClose);
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<MainUI.App>()
            .UsePlatformDetect()
            .LogToTrace();
    }
}
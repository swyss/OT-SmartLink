using AgentModbus;
using AgentMQTT;
using AgentOPCUA;
using Core.Broker;
using Core.Logging;
using Core.Repositories;
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

        // Initialize ServiceConfigRepository and InfluxDBLogger
        var configRepository =
            new ServiceConfigRepository(
                "Host=localhost;Database=service_config;Username=postgres;Password=yourpassword");
        var influxLogger = new InfluxDBLogger("http://localhost:8086", "your-influxdb-token", "my-bucket", "my-org");

        // Create worker services with all necessary dependencies
        var modbusWorker = new ModbusWorker(loggerFactory.CreateLogger<ModbusWorker>(), configRepository, influxLogger);
        var mqttWorker = new MQTTWorker(loggerFactory.CreateLogger<MQTTWorker>(), configRepository, influxLogger);
        var opcuaWorker = new OPCUAWorker(loggerFactory.CreateLogger<OPCUAWorker>(), configRepository, influxLogger);
        var dataStorageWorker = new DataStorageWorker(loggerFactory.CreateLogger<DataStorageWorker>(), configRepository,
            influxLogger);
        var monitoringWorker = new MonitoringWorker(loggerFactory.CreateLogger<MonitoringWorker>(), configRepository,
            influxLogger);
        var securityWorker =
            new SecurityWorker(loggerFactory.CreateLogger<SecurityWorker>(), configRepository, influxLogger);

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
        MainUI.Program.Main(new string[0]);
    }
}
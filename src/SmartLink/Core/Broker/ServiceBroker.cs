using Core.Interfaces;

namespace Core.Broker;

public class ServiceBroker
{
    // Private fields for internal use

    // Constructor to initialize the ServiceBroker with worker services
    public ServiceBroker(IWorkerService modbusWorker, IWorkerService mqttWorker, IWorkerService opcuaWorker,
        IWorkerService dataStorageWorker, IWorkerService monitoringWorker, IWorkerService securityWorker)
    {
        ModbusWorker = modbusWorker;
        MQTTWorker = mqttWorker;
        OPCUAWorker = opcuaWorker;
        DataStorageWorker = dataStorageWorker;
        MonitoringWorker = monitoringWorker;
        SecurityWorker = securityWorker;
    }

    // Public properties to expose worker services
    public IWorkerService ModbusWorker { get; }

    public IWorkerService MQTTWorker { get; }

    public IWorkerService OPCUAWorker { get; }

    public IWorkerService DataStorageWorker { get; }

    public IWorkerService MonitoringWorker { get; }

    public IWorkerService SecurityWorker { get; }

    // Start all services
    public void StartAllServices()
    {
        ModbusWorker.StartService(default);
        MQTTWorker.StartService(default);
        OPCUAWorker.StartService(default);
        DataStorageWorker.StartService(default);
        MonitoringWorker.StartService(default);
        SecurityWorker.StartService(default);
    }

    // Stop all services
    public void StopAllServices()
    {
        ModbusWorker.StopService(default);
        MQTTWorker.StopService(default);
        OPCUAWorker.StopService(default);
        DataStorageWorker.StopService(default);
        MonitoringWorker.StopService(default);
        SecurityWorker.StopService(default);
    }
}
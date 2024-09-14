using Core.Interfaces;

namespace Core.Broker;

public class ServiceBroker
{
    public IWorkerService ModbusWorker { get; }
    public IWorkerService MQTTWorker { get; }
    public IWorkerService OPCUAWorker { get; }
    public IWorkerService DataStorageWorker { get; }
    public IWorkerService MonitoringWorker { get; }
    public IWorkerService SecurityWorker { get; }

    // Constructor using IWorkerService interface
    public ServiceBroker(
        IWorkerService modbusWorker, 
        IWorkerService mqttWorker, 
        IWorkerService opcuaWorker,
        IWorkerService dataStorageWorker,
        IWorkerService monitoringWorker,
        IWorkerService securityWorker)
    {
        ModbusWorker = modbusWorker;
        MQTTWorker = mqttWorker;
        OPCUAWorker = opcuaWorker;
        DataStorageWorker = dataStorageWorker;
        MonitoringWorker = monitoringWorker;
        SecurityWorker = securityWorker;
    }

    // Method to start all services
    public void StartAllServices()
    {
        ModbusWorker.StartService();
        MQTTWorker.StartService();
        OPCUAWorker.StartService();
        DataStorageWorker.StartService();
        MonitoringWorker.StartService();
        SecurityWorker.StartService();
    }

    // Method to stop all services
    public void StopAllServices()
    {
        ModbusWorker.StopService();
        MQTTWorker.StopService();
        OPCUAWorker.StopService();
        DataStorageWorker.StopService();
        MonitoringWorker.StopService();
        SecurityWorker.StopService();
    }

    // Method to get the status of all services
    public string GetAllServicesStatus()
    {
        var statuses = new[]
        {
            $"ModbusWorker: {ModbusWorker.GetServiceStateAsString()}",
            $"MQTTWorker: {MQTTWorker.GetServiceStateAsString()}",
            $"OPCUAWorker: {OPCUAWorker.GetServiceStateAsString()}",
            $"DataStorageWorker: {DataStorageWorker.GetServiceStateAsString()}",
            $"MonitoringWorker: {MonitoringWorker.GetServiceStateAsString()}",
            $"SecurityWorker: {SecurityWorker.GetServiceStateAsString()}"
        };

        return string.Join("\n", statuses);
    }
}

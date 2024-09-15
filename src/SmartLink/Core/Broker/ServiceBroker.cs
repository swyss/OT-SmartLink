using Core.Interfaces;

namespace Core.Broker;

public class ServiceBroker
{
    // Private fields for internal use
    private readonly IWorkerService _modbusWorker;
    private readonly IWorkerService _mqttWorker;
    private readonly IWorkerService _opcuaWorker;
    private readonly IWorkerService _dataStorageWorker;
    private readonly IWorkerService _monitoringWorker;
    private readonly IWorkerService _securityWorker;

    // Public properties to expose worker services
    public IWorkerService ModbusWorker => _modbusWorker;
    public IWorkerService MQTTWorker => _mqttWorker;
    public IWorkerService OPCUAWorker => _opcuaWorker;
    public IWorkerService DataStorageWorker => _dataStorageWorker;
    public IWorkerService MonitoringWorker => _monitoringWorker;
    public IWorkerService SecurityWorker => _securityWorker;

    // Constructor to initialize the ServiceBroker with worker services
    public ServiceBroker(IWorkerService modbusWorker, IWorkerService mqttWorker, IWorkerService opcuaWorker,
        IWorkerService dataStorageWorker, IWorkerService monitoringWorker, IWorkerService securityWorker)
    {
        _modbusWorker = modbusWorker;
        _mqttWorker = mqttWorker;
        _opcuaWorker = opcuaWorker;
        _dataStorageWorker = dataStorageWorker;
        _monitoringWorker = monitoringWorker;
        _securityWorker = securityWorker;
    }

    // Start all services
    public void StartAllServices()
    {
        _modbusWorker.StartService(default);
        _mqttWorker.StartService(default);
        _opcuaWorker.StartService(default);
        _dataStorageWorker.StartService(default);
        _monitoringWorker.StartService(default);
        _securityWorker.StartService(default);
    }

    // Stop all services
    public void StopAllServices()
    {
        _modbusWorker.StopService(default);
        _mqttWorker.StopService(default);
        _opcuaWorker.StopService(default);
        _dataStorageWorker.StopService(default);
        _monitoringWorker.StopService(default);
        _securityWorker.StopService(default);
    }
}
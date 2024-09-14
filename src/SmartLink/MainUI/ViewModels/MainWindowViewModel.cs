using Core.Broker;

namespace MainUI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ServiceBroker ServiceBroker { get; }

    public MainWindowViewModel(ServiceBroker serviceBroker)
    {
        ServiceBroker = serviceBroker;
    }

    public string Greeting => "Welcome to SmartLink!";

    // Display individual worker service statuses
    public string ModBusWorkerServiceStatus => "ModBus Worker Status: " + ServiceBroker.ModbusWorker.GetServiceStateAsString();
    public string MQTTWorkerServiceStatus => "MQTT Worker Status: " + ServiceBroker.MQTTWorker.GetServiceStateAsString();
    public string OPCUAWorkerServiceStatus => "OPC UA Worker Status: " + ServiceBroker.OPCUAWorker.GetServiceStateAsString();
    public string DataStorageWorkerServiceStatus => "Data Storage Worker Status: " + ServiceBroker.DataStorageWorker.GetServiceStateAsString();
    public string MonitoringWorkerServiceStatus => "Monitoring Worker Status: " + ServiceBroker.MonitoringWorker.GetServiceStateAsString();
    public string SecurityWorkerServiceStatus => "Security Worker Status: " + ServiceBroker.SecurityWorker.GetServiceStateAsString();
}
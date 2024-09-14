namespace OTSmartLink.Core.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // Property to hold the greeting
    public string Greeting => "Welcome to OT-SmartLink!";
        
    // Field to hold the worker instance
    private OTSmartLink.Agents.Modbus.Worker _modBusWorker;
        
    // Constructor to initialize the worker
    public MainWindowViewModel()
    {
        // Initialize the ModbusWorker (here we assume you use the parameterless constructor)
        _modBusWorker = new OTSmartLink.Agents.Modbus.Worker();
    }

    // Property to expose the ModBusWorker service status as a string
    public string ModBusWorkerServiceStatus => "ModBusWorker Service Status: " + _modBusWorker.GetServiceStateAsString();

    // Placeholder values for other worker services
    public string WorkerService2Status => "Not Running";
    public string WorkerService3Status => "Not Running";
    public string WorkerService4Status => "Not Running";
    public string WorkerService5Status => "Not Running";
    public string WorkerService6Status => "Not Running";
}
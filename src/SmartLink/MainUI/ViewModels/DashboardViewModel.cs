using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Core.Broker;

namespace MainUI.ViewModels;

public class DashboardViewModel: ViewModelBase
{
    private readonly ServiceBroker _serviceBroker;

    public DashboardViewModel(ServiceBroker serviceBroker)
    {
        _serviceBroker = serviceBroker;

        // Initialize example dashboard data or statuses
        RefreshCommand = new RelayCommand(RefreshData);

        // Example: Load some initial dashboard data
        RefreshData();
    }

    // Collection for displaying system-wide statistics or worker statuses
    public ObservableCollection<string> WorkerStatuses { get; } = new ObservableCollection<string>();

    // Example command to refresh the dashboard data
    public ICommand RefreshCommand { get; }

    // Refresh method for updating the dashboard data (e.g., worker statuses)
    private void RefreshData()
    {
        WorkerStatuses.Clear();
        WorkerStatuses.Add($"Modbus Worker: {_serviceBroker.ModbusWorker.GetServiceStateAsString()}");
        WorkerStatuses.Add($"MQTT Worker: {_serviceBroker.MQTTWorker.GetServiceStateAsString()}");
        WorkerStatuses.Add($"OPC UA Worker: {_serviceBroker.OPCUAWorker.GetServiceStateAsString()}");
        WorkerStatuses.Add($"Data Storage Worker: {_serviceBroker.DataStorageWorker.GetServiceStateAsString()}");
        WorkerStatuses.Add($"Monitoring Worker: {_serviceBroker.MonitoringWorker.GetServiceStateAsString()}");
        WorkerStatuses.Add($"Security Worker: {_serviceBroker.SecurityWorker.GetServiceStateAsString()}");
    }
}
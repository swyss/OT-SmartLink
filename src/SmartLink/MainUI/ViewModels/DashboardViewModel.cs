using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Core.Broker;

namespace MainUI.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly ServiceBroker _serviceBroker;

        public DashboardViewModel(ServiceBroker serviceBroker)
        {
            _serviceBroker = serviceBroker;

            // Initialize commands
            RefreshCommand = new RelayCommand(RefreshData);
            StartServiceCommand = new RelayCommand<string>(StartWorkerService);
            StopServiceCommand = new RelayCommand<string>(StopWorkerService);

            // Load initial dashboard data
            RefreshData();
        }

        // Collection for displaying system-wide worker statuses
        public ObservableCollection<string> WorkerStatuses { get; } = new ObservableCollection<string>();

        // Commands for refreshing, starting, and stopping services
        public ICommand RefreshCommand { get; }
        public ICommand StartServiceCommand { get; }
        public ICommand StopServiceCommand { get; }

        // Method for refreshing the dashboard data (e.g., worker statuses)
        private void RefreshData()
        {
            WorkerStatuses.Clear();

            // Update the collection with the current status of each worker service
            WorkerStatuses.Add($"Modbus Worker: {_serviceBroker.ModbusWorker.GetServiceStateAsString()}");
            WorkerStatuses.Add($"MQTT Worker: {_serviceBroker.MQTTWorker.GetServiceStateAsString()}");
            WorkerStatuses.Add($"OPC UA Worker: {_serviceBroker.OPCUAWorker.GetServiceStateAsString()}");
            WorkerStatuses.Add($"Data Storage Worker: {_serviceBroker.DataStorageWorker.GetServiceStateAsString()}");
            WorkerStatuses.Add($"Monitoring Worker: {_serviceBroker.MonitoringWorker.GetServiceStateAsString()}");
            WorkerStatuses.Add($"Security Worker: {_serviceBroker.SecurityWorker.GetServiceStateAsString()}");
        }

        // Start worker service based on the command parameter
        private void StartWorkerService(string workerName)
        {
            switch (workerName)
            {
                case "Modbus":
                    _serviceBroker.ModbusWorker.StartService(default);
                    break;
                case "MQTT":
                    _serviceBroker.MQTTWorker.StartService(default);
                    break;
                case "OPCUA":
                    _serviceBroker.OPCUAWorker.StartService(default);
                    break;
                case "DataStorage":
                    _serviceBroker.DataStorageWorker.StartService(default);
                    break;
                case "Monitoring":
                    _serviceBroker.MonitoringWorker.StartService(default);
                    break;
                case "Security":
                    _serviceBroker.SecurityWorker.StartService(default);
                    break;
            }

            // Refresh the data after starting the service
            RefreshData();
        }

        // Stop worker service based on the command parameter
        private void StopWorkerService(string workerName)
        {
            switch (workerName)
            {
                case "Modbus":
                    _serviceBroker.ModbusWorker.StopService(default);
                    break;
                case "MQTT":
                    _serviceBroker.MQTTWorker.StopService(default);
                    break;
                case "OPCUA":
                    _serviceBroker.OPCUAWorker.StopService(default);
                    break;
                case "DataStorage":
                    _serviceBroker.DataStorageWorker.StopService(default);
                    break;
                case "Monitoring":
                    _serviceBroker.MonitoringWorker.StopService(default);
                    break;
                case "Security":
                    _serviceBroker.SecurityWorker.StopService(default);
                    break;
            }

            // Refresh the data after stopping the service
            RefreshData();
        }
    }
}

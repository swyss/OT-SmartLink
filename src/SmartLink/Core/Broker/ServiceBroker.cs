using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Core.Broker
{
    public class ServiceBroker
    {
        // Private readonly fields for worker services
        private readonly IWorkerService _modbusWorker;
        private readonly IWorkerService _mqttWorker;
        private readonly IWorkerService _opcuaWorker;
        private readonly IWorkerService _dataStorageWorker;
        private readonly IWorkerService _monitoringWorker;
        private readonly IWorkerService _securityWorker;
        private readonly ILogger<ServiceBroker> _logger;

        // Constructor to initialize the ServiceBroker with worker services
        public ServiceBroker(
            IWorkerService modbusWorker,
            IWorkerService mqttWorker,
            IWorkerService opcuaWorker,
            IWorkerService dataStorageWorker,
            IWorkerService monitoringWorker,
            IWorkerService securityWorker,
            ILogger<ServiceBroker> logger)
        {
            _modbusWorker = modbusWorker;
            _mqttWorker = mqttWorker;
            _opcuaWorker = opcuaWorker;
            _dataStorageWorker = dataStorageWorker;
            _monitoringWorker = monitoringWorker;
            _securityWorker = securityWorker;
            _logger = logger;
        }

        // Start all services
        public async Task StartAllServicesAsync()
        {
            _logger.LogInformation("Starting all services...");

            try
            {
                await Task.WhenAll(
                    _modbusWorker.StartService(default),
                    _mqttWorker.StartService(default),
                    _opcuaWorker.StartService(default),
                    _dataStorageWorker.StartService(default),
                    _monitoringWorker.StartService(default),
                    _securityWorker.StartService(default)
                );

                _logger.LogInformation("All services started successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while starting services.");
                throw;
            }
        }

        // Stop all services
        public async Task StopAllServicesAsync()
        {
            _logger.LogInformation("Stopping all services...");

            try
            {
                await Task.WhenAll(
                    _modbusWorker.StopService(default),
                    _mqttWorker.StopService(default),
                    _opcuaWorker.StopService(default),
                    _dataStorageWorker.StopService(default),
                    _monitoringWorker.StopService(default),
                    _securityWorker.StopService(default)
                );

                _logger.LogInformation("All services stopped successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while stopping services.");
                throw;
            }
        }
    }
}

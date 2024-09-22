using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Core.Broker;

public class ServiceBroker(
    IWorkerService modbusWorker,
    IWorkerService mqttWorker,
    IWorkerService opcuaWorker,
    IWorkerService dataStorageWorker,
    IWorkerService monitoringWorker,
    IWorkerService securityWorker,
    ILogger<ServiceBroker> logger)
{
    // Private readonly fields for worker services

    // Constructor to initialize the ServiceBroker with worker services

    // Start all services
    public async Task StartAllServicesAsync()
    {
        logger.LogInformation("Starting all services...");

        try
        {
            await Task.WhenAll(
                modbusWorker.StartService(default),
                mqttWorker.StartService(default),
                opcuaWorker.StartService(default),
                dataStorageWorker.StartService(default),
                monitoringWorker.StartService(default),
                securityWorker.StartService(default)
            );

            logger.LogInformation("All services started successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while starting services.");
            throw;
        }
    }

    // Stop all services
    public async Task StopAllServicesAsync()
    {
        logger.LogInformation("Stopping all services...");

        try
        {
            await Task.WhenAll(
                modbusWorker.StopService(default),
                mqttWorker.StopService(default),
                opcuaWorker.StopService(default),
                dataStorageWorker.StopService(default),
                monitoringWorker.StopService(default),
                securityWorker.StopService(default)
            );

            logger.LogInformation("All services stopped successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while stopping services.");
            throw;
        }
    }
}
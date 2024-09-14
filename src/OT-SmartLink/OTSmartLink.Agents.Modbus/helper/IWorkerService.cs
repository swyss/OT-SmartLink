namespace OTSmartLink.Agents.Modbus.helper;

public interface IWorkerService
{
    // Return the current service state as a string
    string GetServiceStateAsString();
    
    // Other methods to control the service
    ServiceState GetServiceState();
    void StartService();
    void StopService();
}
public enum ServiceState
{
    Stopped,
    Running,
    Error,
    Unknown
}
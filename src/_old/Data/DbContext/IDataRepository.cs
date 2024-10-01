using Data.Models;

namespace Data.DbContext;

public interface IDataRepository
{
    Task StoreSensorData(SensorData data);
    Task<IEnumerable<SensorData>> GetRecentSensorData();
}
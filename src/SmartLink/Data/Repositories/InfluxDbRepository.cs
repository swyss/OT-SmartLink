using Data.DbContext;
using Data.Models;
using Data.Services;

namespace Data.Repositories;

public class InfluxDbRepository : IDataRepository
{
    private readonly InfluxDbService _influxDbService;

    public InfluxDbRepository(InfluxDbService influxDbService)
    {
        _influxDbService = influxDbService;
    }

    public async Task StoreSensorData(SensorData data)
    {
        await _influxDbService.WriteSensorDataAsync("your_bucket", "your_org", data);
    }

    public Task<IEnumerable<SensorData>> GetRecentSensorData()
    {
        // Implement InfluxDB query logic here
        throw new NotImplementedException();
    }
}
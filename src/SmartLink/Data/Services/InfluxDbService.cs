using Data.DbContext;
using Data.Models;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;

namespace Data.Services;

public class InfluxDbService
{
    private readonly InfluxDBClient _client;

    public InfluxDbService(string url, string token)
    {
        _client = InfluxDBClientFactory.Create(url, token);
    }

    public async Task WriteSensorDataAsync(string bucket, string org, SensorData data)
    {
        var writeApi = _client.GetWriteApiAsync();
        var point = PointData.Measurement("sensor_data")
            .Tag("sensor", data.SensorName)
            .Field("value", data.Value)
            .Timestamp(data.Timestamp, WritePrecision.Ns);
        await writeApi.WritePointAsync(point, bucket, org);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
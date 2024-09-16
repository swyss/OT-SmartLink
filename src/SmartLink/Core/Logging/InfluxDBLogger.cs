using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;

namespace Core.Logging;

public class InfluxDBLogger(string url, string token, string bucket, string org)
{
    private readonly InfluxDBClient _client = new(url, token);

    // Log service status to InfluxDB
    public async Task LogServiceStatus(string serviceName, string status, DateTime timestamp)
    {
        var writeApi = _client.GetWriteApiAsync();

        var point = PointData.Measurement("service_status")
            .Tag("service", serviceName)
            .Field("status", status)
            .Timestamp(timestamp, WritePrecision.Ms);

        await writeApi.WritePointAsync(point, bucket, org);
    }
}
using InfluxDB.Client;
using InfluxDB.Client.Writes;
using System;
using System.Threading.Tasks;
using InfluxDB.Client.Api.Domain;

namespace Core.Logging;

public class InfluxDBLogger
{
    private readonly InfluxDBClient _client;
    private readonly string _bucket;
    private readonly string _org;

    public InfluxDBLogger(string url, string token, string bucket, string org)
    {
        _client = InfluxDBClientFactory.Create(url, token);
        _bucket = bucket;
        _org = org;
    }

    // Log service status to InfluxDB
    public async Task LogServiceStatus(string serviceName, string status, DateTime timestamp)
    {
        var writeApi = _client.GetWriteApiAsync();

        var point = PointData.Measurement("service_status")
            .Tag("service", serviceName)
            .Field("status", status)
            .Timestamp(timestamp, WritePrecision.Ms);

        await writeApi.WritePointAsync(point, _bucket, _org);
    }
}
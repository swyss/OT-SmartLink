using Npgsql;
using System.Threading.Tasks;

namespace Core.Repositories;

public class ServiceConfigRepository
{
    private readonly string _connectionString;

    public ServiceConfigRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Save configuration to PostgreSQL
    public async Task SaveConfigAsync(string serviceName, string key, string value)
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();
        var cmd = new NpgsqlCommand(
            "INSERT INTO service_config (service_name, config_key, config_value) VALUES (@service, @key, @value)",
            conn);
        cmd.Parameters.AddWithValue("service", serviceName);
        cmd.Parameters.AddWithValue("key", key);
        cmd.Parameters.AddWithValue("value", value);
        await cmd.ExecuteNonQueryAsync();
    }

    // Load configuration from PostgreSQL
    public async Task<string> LoadConfigAsync(string serviceName, string key)
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();
        var cmd = new NpgsqlCommand(
            "SELECT config_value FROM service_config WHERE service_name = @service AND config_key = @key", conn);
        cmd.Parameters.AddWithValue("service", serviceName);
        cmd.Parameters.AddWithValue("key", key);
        var result = await cmd.ExecuteScalarAsync();
        return result?.ToString();
    }
}
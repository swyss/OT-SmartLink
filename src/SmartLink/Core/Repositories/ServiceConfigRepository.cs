using Npgsql;
using Microsoft.Extensions.Options;

namespace Core.Repositories
{
    public class ServiceConfigRepository : IServiceConfigRepository
    {
        private readonly string _connectionString;

        public ServiceConfigRepository(IOptions<DatabaseSettings> dbSettings)
        {
            _connectionString = dbSettings.Value.ConnectionString;

            // Initialize database and table
            EnsureDatabaseAndTableCreated().Wait();
        }

        // Ensure that the database and table exist with a partial unique index
        private async Task EnsureDatabaseAndTableCreated()
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentException("Connection string cannot be null or empty");

            var databaseName = new NpgsqlConnectionStringBuilder(_connectionString).Database;

            // Ensure that the PostgreSQL database exists
            var masterConnectionString = new NpgsqlConnectionStringBuilder(_connectionString)
            {
                Database = "postgres" // The default PostgreSQL system database
            }.ToString();

            await using var masterConnection = new NpgsqlConnection(masterConnectionString);
            await masterConnection.OpenAsync();

            // Check if the database exists, create it if it doesn't
            var dbCheckCmd = new NpgsqlCommand($"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'", masterConnection);
            var dbExists = await dbCheckCmd.ExecuteScalarAsync() != null;

            if (!dbExists)
            {
                var createDbCmd = new NpgsqlCommand($"CREATE DATABASE \"{databaseName}\"", masterConnection);
                await createDbCmd.ExecuteNonQueryAsync();
            }

            // Now switch to the application's database
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            // Ensure the table exists with a partial UNIQUE index
            var tableCheckCmd = new NpgsqlCommand(@"
                CREATE TABLE IF NOT EXISTS service_config (
                    id SERIAL PRIMARY KEY,
                    service_name TEXT NOT NULL,
                    config_key TEXT NOT NULL,
                    config_value TEXT NOT NULL
                );

                CREATE UNIQUE INDEX IF NOT EXISTS unique_service_key_partial 
                ON service_config (service_name, config_key) 
                WHERE (config_key = 'special');", conn);

            await tableCheckCmd.ExecuteNonQueryAsync();
        }

        // Save configuration to PostgreSQL
        public async Task SaveConfigAsync(string serviceName, string key, string value)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            var cmd = new NpgsqlCommand(
                "INSERT INTO service_config (service_name, config_key, config_value) VALUES (@service, @key, @value) " +
                "ON CONFLICT (service_name, config_key) WHERE (config_key = 'special') DO UPDATE SET config_value = @value", conn);
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
            return result?.ToString() ?? throw new InvalidOperationException($"Configuration for {serviceName} and {key} not found.");
        }
    }
}

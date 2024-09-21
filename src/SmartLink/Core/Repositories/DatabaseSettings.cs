namespace Core.Repositories
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = "Host=localhost;Database=service_config;Username=postgres;Password=postgres";
    }
}
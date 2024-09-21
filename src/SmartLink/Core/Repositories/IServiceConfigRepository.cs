namespace Core.Repositories
{
    public interface IServiceConfigRepository
    {
        Task SaveConfigAsync(string serviceName, string key, string value);
        Task<string> LoadConfigAsync(string serviceName, string key);
    }
}
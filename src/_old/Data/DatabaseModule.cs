using Data.Helpers;

namespace Data;

public class DatabaseModule
{
    private readonly DockerHelper _dockerHelper;

    public DatabaseModule()
    {
        _dockerHelper = new DockerHelper();
    }

    public async Task InitializeDatabaseAsync()
    {
        // Start Docker environment if necessary
        await _dockerHelper.StartDockerEnvironmentAsync();
            
        // You can add further initialization logic here if needed
    }
}
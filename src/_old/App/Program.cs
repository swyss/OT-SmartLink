using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        // Initialize application starter and build the host
        var applicationStarter = new ApplicationStarter();
        var host = applicationStarter.CreateHostBuilder(args).Build();

        // Start application and services with ConsoleHandler
        var consoleHandler = host.Services.GetRequiredService<ConsoleHandler>();
        await consoleHandler.StartApplicationAsync(host);

        // Run the host (includes background worker services)
        await host.RunAsync();
    }
}
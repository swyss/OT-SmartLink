using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using System.Threading.Tasks;

namespace App
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            // Display ASCII art logo
            DisplayLogo();

            // Start the application, services, and UI
            var applicationStarter = new ApplicationStarter();
            var host = applicationStarter.CreateHostBuilder(args).Build();

            // Start application and services with a unified status display
            await StartWithStatus(applicationStarter, host);

            // Run the host (which includes background worker services)
            await host.RunAsync();
        }

        private static void DisplayLogo()
        {
            AnsiConsole.Write(
                new FigletText("STARTER OF")
                    //.LeftAligned()
                    .Color(Color.Aqua));

            AnsiConsole.MarkupLine("[bold blue]   ______  ______   ___  ______    __   _      __  [/]");
            AnsiConsole.MarkupLine("[bold blue]  / __/  |/  / _ | / _ \\/_  __/___/ /  (_)__  / /__[/]");
            AnsiConsole.MarkupLine("[bold blue] _\\ \\/ /|_/ / __ |/ , _/ / / /___/ /__/ / _ \\/  '_/[/]");
            AnsiConsole.MarkupLine("[bold blue]/___/_/  /_/_/ |_/_/|_| /_/     /____/_/_//_/_/\\_\\[/]");
            AnsiConsole.MarkupLine("[bold yellow]-----------------------------------------------[/]\n");
        }

        private static async Task StartWithStatus(ApplicationStarter applicationStarter, IHost host)
        {
            AnsiConsole.Markup("[bold yellow]Starting application and services...[/]\n");

            // Use Spectre.Console to show spinners and checkmarks for services and UI
            await AnsiConsole.Status()
                .StartAsync("Initializing application...", async ctx =>
                {
                    ctx.Spinner(Spinner.Known.Dots);
                    ctx.SpinnerStyle(Style.Parse("green"));

                    // Start services and UI in ApplicationStarter
                    await applicationStarter.StartAsync(host);

                    AnsiConsole.Markup("[bold green]✔ All services and UI started successfully![/]\n");
                });
        }
    }
}

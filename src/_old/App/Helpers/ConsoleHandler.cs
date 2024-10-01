using Microsoft.Extensions.Hosting;
using Spectre.Console;
using System;
using System.Threading.Tasks;
using Core.Orchestrator;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public class ConsoleHandler
    {
        // Displays the application logo in the console
        public void DisplayLogo()
        {
            AnsiConsole.Write(
                new FigletText("...")
                    .Color(Color.Aqua));

            AnsiConsole.MarkupLine("[bold blue]   ______  ______   ___  ______    __   _      __  [/]");
            AnsiConsole.MarkupLine("[bold blue]  / __/  |/  / _ | / _ \\/_  __/___/ /  (_)__  / /__[/]");
            AnsiConsole.MarkupLine("[bold blue] _\\ \\/ /|_/ / __ |/ , _/ / / /___/ /__/ / _ \\/  '_/[/]");
            AnsiConsole.MarkupLine("[bold blue]/___/_/  /_/_/ |_/_/|_| /_/     /____/_/_//_/_/\\_\\[/]");
            AnsiConsole.MarkupLine("[bold yellow]-----------------------------------------------[/]\\n");
        }

        // Starts the application progress display in the console
        public async Task StartApplicationAsync(IHost host)
        {
            // Display ASCII art logo
            DisplayLogo();

            // Use Spectre.Console to show spinners and checkmarks for starting services and UI
            await AnsiConsole.Status()
                .StartAsync("Initializing application...", async ctx =>
                {
                    ctx.Spinner(Spinner.Known.Dots);
                    ctx.SpinnerStyle(Style.Parse("green"));

                    // Log the progress of starting services and agents
                    AnsiConsole.Markup("[bold yellow]Starting services and agents...[/]\\n");

                    // Start orchestrator for services (delegated to ApplicationStarter)
                    var orchestrator = host.Services.GetRequiredService<IOrchestrator>();
                    await orchestrator.StartAsync();

                    AnsiConsole.Markup("[bold green]✔ Services and agents started successfully.[/]\\n");
                });
        }

        // Provides an error output in case of failure
        public void ReportError(string errorMessage)
        {
            AnsiConsole.Markup($"[bold red]Error: {errorMessage}[/]\\n");
        }

        // Provides feedback on stopping the application
        public async Task StopApplicationAsync(IHost host)
        {
            AnsiConsole.Markup("[bold yellow]Stopping services and agents...[/]\\n");

            // Stop all services using the orchestrator
            var orchestrator = host.Services.GetRequiredService<IOrchestrator>();
            await orchestrator.StopAsync();

            AnsiConsole.Markup("[bold green]✔ All services and agents stopped.[/]\\n");
        }
    }
}

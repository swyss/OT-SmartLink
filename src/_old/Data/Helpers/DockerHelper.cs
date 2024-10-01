using System.Diagnostics;

namespace Data.Helpers;

public class DockerHelper
{
    public async Task StartDockerEnvironmentAsync()
    {
        if (!IsDockerRunning())
        {
            await RunDockerComposeUpAsync();
        }
    }

    private bool IsDockerRunning()
    {
        // Logic to check if Docker is running
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "docker",
                Arguments = "ps",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        // Check if PostgreSQL and InfluxDB are already running
        return output.Contains("postgres") && output.Contains("influxdb");
    }

    private Task RunDockerComposeUpAsync()
    {
        return Task.Run(() =>
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "docker-compose",
                    Arguments = "up -d",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = "path_to_your_docker_compose_directory"
                }
            };

            process.Start();
            process.WaitForExit();
        });
    }
}
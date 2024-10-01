namespace App;

public class StartupSettings
{
    public int MaxRetryCount { get; set; }
    public int RetryIntervalSeconds { get; set; }
    public int HealthCheckIntervalSeconds { get; set; }
}

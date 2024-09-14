using OTSmartLink.Agents.Modbus.helper;

namespace OTSmartLink.Agents.Modbus;
 public class Worker : BackgroundService, IWorkerService
    {
        private readonly ILogger<Worker> _logger;
        private ServiceState _currentState;

        // Main constructor that accepts a logger
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _currentState = ServiceState.Stopped; // Initial state
        }

        // Parameterless constructor with a default logger
        public Worker() 
        {
            // Create a default logger if none is provided
            using var loggerFactory = LoggerFactory.Create(builder => 
            {
                builder.AddConsole(); // Add more logging providers if needed
            });
            
            _logger = loggerFactory.CreateLogger<Worker>();
            _currentState = ServiceState.Stopped; // Initial state
        }

        public string GetServiceStateAsString()
        {
            return _currentState.ToString(); // Converts the enum state to string
        }

        public ServiceState GetServiceState()
        {
            return _currentState;
        }

        public void StartService()
        {
            _currentState = ServiceState.Running;
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Service started at: {time}", DateTimeOffset.Now);
            }
        }

        public void StopService()
        {
            _currentState = ServiceState.Stopped;
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Service stopped at: {time}", DateTimeOffset.Now);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StartService();

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    }

                    // Simulating a task
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _currentState = ServiceState.Error;
                _logger.LogError(ex, "An error occurred in the worker service.");
            }
            finally
            {
                StopService();
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            StopService();
            await base.StopAsync(stoppingToken);
        }
    }
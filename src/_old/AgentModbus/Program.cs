using AgentModbus;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ModbusWorker>();

var host = builder.Build();
host.Run();
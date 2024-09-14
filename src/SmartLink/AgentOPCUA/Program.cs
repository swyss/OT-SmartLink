using AgentOPCUA;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<OPCUAWorker>();

var host = builder.Build();
host.Run();
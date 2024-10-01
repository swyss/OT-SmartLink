using AgentOPCUA;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<OpcuaWorker>();

var host = builder.Build();
host.Run();
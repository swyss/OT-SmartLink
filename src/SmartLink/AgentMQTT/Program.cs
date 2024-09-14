using AgentMQTT;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<MQTTWorker>();

var host = builder.Build();
host.Run();
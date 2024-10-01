using ServiceDataStorage;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<DataStorageWorker>();

var host = builder.Build();
host.Run();
using ServiceSecurity;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<SecurityWorker>();

var host = builder.Build();
host.Run();
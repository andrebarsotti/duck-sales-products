using DuckSales.Hosts.ProductWorker;
using DuckSales.Hosts.ProductWorker.Services;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddScoped<IProductChangesSimulationService, ProductChangesSimulationService>();
    })
    .UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration))
    .Build();

await host.RunAsync();

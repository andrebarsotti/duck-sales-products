using DuckSales.Hosts.ProductWorker;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(GetAppConfiguration(args))
    .CreateLogger();

try
{
    Log.Information("Starting host...");
    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
        })
        .UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration))
        .Build();

    Log.Information("Host created, running app...");
    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

static IConfiguration GetAppConfiguration(string[] args)
{
    string enviroment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

    configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{enviroment}.json", true);

    if (enviroment == "Development")
        configurationBuilder.AddUserSecrets<Program>(optional: true);

    configurationBuilder.AddEnvironmentVariables()
        .AddCommandLine(args);

    return configurationBuilder.Build();
}

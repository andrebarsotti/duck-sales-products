using DuckSales.Infra.ProductsDataBase;
using Serilog;

namespace DuckSales.Hosts.ProductWorker;

public static class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(GetAppConfiguration(args))
            .CreateLogger();

        try
        {
            Log.Information("Starting host...");
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Clear configs to work properly in linux enviroments.
                    config.Sources.Clear();
                    PreparerConfigBuilder(config, context.HostingEnvironment.EnvironmentName, args);
                })
                .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
                .UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration))
                .Build();

            host.Services
                .CreateScope()
                .ServiceProvider
                .GetService<ProductsDBContext>()?.Database?.EnsureCreated();
            Log.Information("Database is created");

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
    }

    private static IConfiguration GetAppConfiguration(string[] args)
    {
        string enviroment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

        PreparerConfigBuilder(configurationBuilder, enviroment, args);

        return configurationBuilder.Build();
    }

    private static void PreparerConfigBuilder(IConfigurationBuilder configurationBuilder, string enviroment, string[] args)
    {
        configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{enviroment}.json", optional: true, reloadOnChange: false);

        if (enviroment == "Development")
            configurationBuilder.AddUserSecrets<Startup>(optional: true);

        configurationBuilder.AddEnvironmentVariables()
            .AddCommandLine(args);
    }
}


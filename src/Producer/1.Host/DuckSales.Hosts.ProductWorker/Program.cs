using DuckSales.Application.CommandHandlers.Extensions;
using DuckSales.Application.QueryHandlers.Extensions;
using DuckSales.Domains.Products.Extensions;
using DuckSales.Hosts.ProductWorker;
using DuckSales.Hosts.ProductWorker.Behaviors;
using DuckSales.Hosts.ProductWorker.Services;
using DuckSales.Infra.ProductsDataBase;
using DuckSales.Infra.ProductsDataBase.Extensions;
using MediatR;
using Serilog;

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
            config.Sources.Clear();
            PreparerConfigBuilder(config, context.HostingEnvironment.EnvironmentName, args);
        })
        .ConfigureServices((host, services) =>
        {
            services.AddHostedService<Worker>();
            services.AddScoped<IProductChangesSimulationService, ProductChangesSimulationService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.Configure<ProductWorkerSettings>(host.Configuration.GetSection(nameof(ProductWorkerSettings)));

            services.Configure<ProductWorkerSettings>(host.Configuration);

            services.AddCommannds();
            services.AddQueries();
            services.AddDomainServices();
            services.AddRepositories(host.Configuration);
        })
        .UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration))
        .Build();

    host.Services
        .CreateScope()
        .ServiceProvider
        .GetService<ProductsDBContext>()?.Database?.EnsureCreated();

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

    PreparerConfigBuilder(configurationBuilder, enviroment, args);

    return configurationBuilder.Build();
}

static void PreparerConfigBuilder(IConfigurationBuilder configurationBuilder, string enviroment, string[] args)
{
    configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
        .AddJsonFile($"appsettings.{enviroment}.json", optional: true, reloadOnChange: false);

    if (enviroment == "Development")
        configurationBuilder.AddUserSecrets<Program>(optional: true);

    configurationBuilder.AddEnvironmentVariables()
        .AddCommandLine(args);
}

using DuckSales.Application.CommandHandlers.Extensions;
using DuckSales.Application.QueryHandlers.Extensions;
using DuckSales.Domains.Products.Extensions;
using DuckSales.Hosts.ProductWorker.Behaviors;
using DuckSales.Hosts.ProductWorker.Services;
using DuckSales.Infra.ProductsDataBase;
using DuckSales.Infra.ProductsDataBase.Extensions;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Serilog;
using ILogger = Serilog.ILogger;

namespace DuckSales.Hosts.ProductWorker;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
        _logger = Log.Logger.ForContext<Startup>();
    }

    public virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddHostedService<Worker>();
        _logger.Information("Worker configured...");

        services.AddScoped<IProductChangesSimulationService, ProductChangesSimulationService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        _logger.Information("Host services configured...");

        services.Configure<ProductWorkerSettings>(_configuration.GetSection(nameof(ProductWorkerSettings)))
                .AddOptions();
        _logger.Information("Host settings configured...");

        services.AddCommannds();
        _logger.Information("Commands configured...");

        services.AddQueries();
        _logger.Information("Queries configured...");

        services.AddDomainServices();
        _logger.Information("Domain services configured...");

        services.AddRepositories(_configuration);
        _logger.Information("Repositories configured...");

        services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(_configuration.GetConnectionString(DatabaseConstants.DataBaseConnection),
                              name: "duck-sales-product-db",
                              tags: new [] { "deps"})
                //.AddKafka()
                ;
        _logger.Information("HealthChecks configured...");

        _logger.Information("Services configured");
    }

    public void Configure(IApplicationBuilder app, ProductsDBContext dbContext)
    {
        app.UseRouting();
        _logger.Information("Using routing...");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });
        });
        _logger.Information("Endpoints configured");

        _logger.Information("Application configured");
    }

}

using DuckSales.Hosts.ProductWorker.Services;

namespace DuckSales.Hosts.ProductWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider,
                  ILogger<Worker> logger)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
                await ExecuteScopedService(stoppingToken);
        }
        catch (Exception exception)
        {
            _logger.LogCritical(exception, "Critical Exceptions running Product Simulation");
            Environment.Exit(1);
        }
    }

    private async Task ExecuteScopedService(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Running the Product Simulation Service...");
        using IServiceScope scope = _serviceProvider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IProductChangesSimulationService>();
        await service.Execute(cancellationToken);
        _logger.LogInformation("Product Simulation Service executed.");
    }
}

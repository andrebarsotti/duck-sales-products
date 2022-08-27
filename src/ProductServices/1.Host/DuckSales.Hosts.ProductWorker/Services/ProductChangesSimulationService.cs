using Bogus;
using DuckSales.Application.Commands;
using DuckSales.Application.Queries;
using MediatR;
using Microsoft.Extensions.Options;

namespace DuckSales.Hosts.ProductWorker.Services;

public interface IProductChangesSimulationService
{
    Task Execute(CancellationToken cancellationToken);
}

public sealed class ProductChangesSimulationService : IProductChangesSimulationService
{
    private const int DefaultWait = 1000;
    private readonly IMediator _mediator;
    private readonly Faker _faker;
    private readonly ProductWorkerSettings _settings;
    private readonly ILogger<ProductChangesSimulationService> _logger;

    public ProductChangesSimulationService(IMediator mediator,
        IOptions<ProductWorkerSettings> options,
        ILogger<ProductChangesSimulationService> logger)
    {
        _mediator = mediator;
        _faker = new Faker();
        _logger = logger;
        _settings = options.Value;
        _logger.LogDebug("ProductWorkerSettings => {@Setting}", _settings);
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        try
        {
            if (ShouldUpdateAProduct(await GetTotalAmountOfProdcuntFromCatalog()))
            {
                await UpdateAProduct();
            }
            else
            {
                await CreateNewProduct();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error running simulation");
        }

        await Task.Delay(_settings.WaitingTimeBeferoNextCall ?? DefaultWait, cancellationToken);
    }

    private async Task CreateNewProduct()
    {
        _logger.LogDebug("Creating a new product...");
        await _mediator.Send(new CreateProductCommand(_faker.Commerce.ProductName(),
            _faker.Commerce.Department(max: 1),
            _faker.Random.Int(0),
            _faker.Finance.Amount()));
        _logger.LogDebug("Product created");
    }

    private async Task UpdateAProduct()
    {

        ProductOfCatalogReadModel product = await GetProductFromDataBase();

        _logger.LogDebug("Updating a product...");
        await _mediator.Send(new UpdateProductCommand(product.ProductId,
            _faker.Random.Int(0),
            _faker.Finance.Amount()));
        _logger.LogDebug("Product updated");
    }

    private async Task<int> GetTotalAmountOfProdcuntFromCatalog()
    {
        _logger.LogDebug("Calculating the total amount of product in database...");
        int total = await _mediator.Send(new GetTotalAmountOfProdcuntFromCatalogQuery());
        _logger.LogDebug("Total amount = {TotalProdcutAmount}", total);
        return total;
    }

    private bool ShouldUpdateAProduct(int amountOfProducts)
    {
        _logger.LogDebug("Checking if a product should be updated...");
        double probabilityOfUpdate = GetChanceOfUpdate(amountOfProducts);
        _logger.LogDebug("Calculated a {ChangeOfUpdate}% chance of update a product...", probabilityOfUpdate * 100d);
        _logger.LogDebug("Throwing the dice...");
        double randomDouble = _faker.Random.Double();
        bool shouldUpdate = randomDouble <= probabilityOfUpdate;
        _logger.LogDebug("Should update a product = {ShoulUpate}", shouldUpdate);
        return  shouldUpdate;
    }

    private async Task<ProductOfCatalogReadModel> GetProductFromDataBase()
    {
        _logger.LogDebug("Getting all products...");
        IEnumerable<ProductOfCatalogReadModel> productList =
            await _mediator.Send(new GetAllProductsFromCatalogQuery());
        _logger.LogDebug("All products getted");

        _logger.LogDebug("Picking a random product to update...");
        ProductOfCatalogReadModel product = _faker.PickRandom(productList);
        _logger.LogDebug("Product {@Product} getted", product);
        return product;
    }

    private double GetChanceOfUpdate(int amountOfProducts)
        => amountOfProducts / (double)_settings.MaximumNumberOfProducts;
}

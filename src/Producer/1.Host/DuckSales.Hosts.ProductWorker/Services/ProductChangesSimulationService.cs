using Bogus;
using DuckSales.Application.Commands;
using DuckSales.Application.Queries;
using MediatR;
using Microsoft.Extensions.Options;

namespace DuckSales.Hosts.ProductWorker.Services;

public interface IProductChangesSimulationService
{
    Task Execute();
}

public sealed class ProductChangesSimulationService : IProductChangesSimulationService
{
    private readonly IMediator _mediator;
    private readonly Faker _faker;
    private readonly ProductWorkerSettings _settings;

    public ProductChangesSimulationService(IMediator mediator,
        IOptions<ProductWorkerSettings> options)
        => (_mediator, _faker, _settings) = (mediator, new Faker(), options.Value);

    public async Task Execute()
    {
        int amountOfProducts = await _mediator.Send(new GetTotalAmountOfProdcuntFromCatalogQuery());

        if (_faker.Random.Double(min: 0D, max: 1D) <= GetChanceOfUpdate(amountOfProducts))
        {
            IEnumerable<ProductOfCatalogReadModel> productList =
                await _mediator.Send(new GetAllProductsFromCatalogQuery());

            ProductOfCatalogReadModel product = _faker.PickRandom(productList);

            await _mediator.Send(new UpdateProductCommand(product.ProductId,
                _faker.Random.Int(0),
                _faker.Finance.Amount()));
        }
        else
        {
            await _mediator.Send(new CreateProductCommand(_faker.Commerce.ProductName(),
                _faker.Commerce.Department(),
                _faker.Random.Int(0),
                _faker.Finance.Amount()));
        }
    }

    private double GetChanceOfUpdate(int amountOfProducts)
        => amountOfProducts / (double)_settings.MaximumNumberOfProducts;
}

using Bogus;
using DuckSales.Application.Commands;
using MediatR;

namespace DuckSales.Hosts.ProductWorker.Services;

public interface IProductChangesSimulationService
{
    Task Execute();
}

public sealed class ProductChangesSimulationService : IProductChangesSimulationService
{
    private readonly IMediator _mediator;
    private readonly Faker _faker;

    public ProductChangesSimulationService(IMediator mediator)
        => (_mediator, _faker) = (mediator, new Faker());

    public async Task Execute()
    {
        var command = new CreateProductCommand()
        {
            ProductName = _faker.Commerce.ProductName(),
            DepartamentName = _faker.Commerce.Department(),
            QuantityAvaiableInStock = _faker.Random.Int(min: 0),
            UnitPrice = _faker.Finance.Amount()
        };

        await _mediator.Send(command);
    }
}

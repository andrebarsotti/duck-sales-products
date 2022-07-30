namespace DuckSales.Application.CommandHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    private readonly IProductsService _productsService;

    public CreateProductCommandHandler(IProductsService productsService)
        => _productsService = productsService;

    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await _productsService.CreateProduct(new NewProductDto(request.ProductName, request.DepartamentName,
            request.QuantityAvaiableInStock,
            request.UnitPrice));

        return  Unit.Value;
    }
}

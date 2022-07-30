namespace DuckSales.Application.CommandHandlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductsService _productsService;

    public UpdateProductCommandHandler(IProductsService productsService)
        => _productsService = productsService;

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await _productsService.UpdateProduct(new ProductUpdateDto(request.ProductId,
            request.QuantityAvaiableInStock,
            request.UnitPrice));

        return Unit.Value;
    }
}

namespace DuckSales.Application.QueryHandlers;

public class GetTotalAmountOfProdcuntFromCatalogQueryHandler: IRequestHandler<GetTotalAmountOfProdcuntFromCatalogQuery, int>
{
    private readonly ProductsDBContext _context;

    public GetTotalAmountOfProdcuntFromCatalogQueryHandler(ProductsDBContext context)
        => _context = context;

    public Task<int> Handle(GetTotalAmountOfProdcuntFromCatalogQuery request, CancellationToken cancellationToken)
        => _context.Products.CountAsync();
}

namespace DuckSales.Application.QueryHandlers;

public class
    GetAllProductsFromCatalogQueryHandler : IRequestHandler<GetAllProductsFromCatalogQuery,
        IEnumerable<ProductOfCatalogReadModel>>
{
    private readonly ProductsDBContext _context;

    public GetAllProductsFromCatalogQueryHandler(ProductsDBContext context)
        => _context = context;

    public async Task<IEnumerable<ProductOfCatalogReadModel>> Handle(GetAllProductsFromCatalogQuery request,
        CancellationToken cancellationToken)
        => await _context.Products
            .Select(prd => new ProductOfCatalogReadModel(prd.Id, prd.Name, prd.Departament!.Name,
                prd.QuantityAvaiableInStock, prd.UnitPrice))
            .ToListAsync(cancellationToken: cancellationToken);
}

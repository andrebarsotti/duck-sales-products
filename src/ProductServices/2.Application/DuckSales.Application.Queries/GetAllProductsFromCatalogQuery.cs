namespace DuckSales.Application.Queries;

public class GetAllProductsFromCatalogQuery: IRequest<IEnumerable<ProductOfCatalogReadModel>> { }

public record ProductOfCatalogReadModel(Guid ProductId,
                                        string ProductName,
                                        string DepartamentName,
                                        int QuantityAvaiableInStock,
                                        decimal UnitPrice);

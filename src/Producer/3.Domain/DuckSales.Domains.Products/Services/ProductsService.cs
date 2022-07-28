namespace DuckSales.Domains.Products.Services;

public interface IProductsService
{
    Task CreateProduct(NewProductDTO product);

    Task UpdateProduct(ProductUpdateDTO product);
}

public record NewProductDTO(string ProductName,
    string DepartamentName,
    int QuantityAvaiableInStock,
    decimal UnitPrice);

public record ProductUpdateDTO(Guid ProductId,
    int QuantityAvaiableInStock,
    decimal UnitPrice);

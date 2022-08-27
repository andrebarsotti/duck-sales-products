using Microsoft.Extensions.Logging;

namespace DuckSales.Domains.Products.Services;

public interface IProductsService
{
    Task CreateProduct(NewProductDto productDto);

    Task UpdateProduct(ProductUpdateDto productDto);
}

public class ProductService : IProductsService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateProduct(NewProductDto productDto)
    {
        Departament? departament = await _repository.GetDepartmentByName(productDto.DepartamentName);

        Product product = new();
        product.SetName(productDto.ProductName);

        if (departament is null)
            product.SetDepartment(productDto.DepartamentName);
        else
            product.SetDepartment(departament);
        product.SetQuantityAvaiableInStock(productDto.QuantityAvaiableInStock);
        product.SetUnitPrice(productDto.UnitPrice);

        await _repository.Add(product);
        await _repository.UnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateProduct(ProductUpdateDto productDto)
    {
        Product product = await _repository.GetById(productDto.ProductId) ??
                          throw new ProductsDomainException(ErrorMessages.ProductNotFound);

        product.SetQuantityAvaiableInStock(productDto.QuantityAvaiableInStock);
        product.SetUnitPrice(productDto.UnitPrice);

        await _repository.Update(product);
        await _repository.UnitOfWork.SaveChangesAsync();
    }
}

public record NewProductDto(string ProductName,
    string DepartamentName,
    int QuantityAvaiableInStock,
    decimal UnitPrice);

public record ProductUpdateDto(Guid ProductId,
    int QuantityAvaiableInStock,
    decimal UnitPrice);

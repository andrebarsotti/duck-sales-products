namespace DuckSales.Domains.Products.Repositories;

public interface IProductRepository
{
    Task Add(Product product);

    Task<Departament?> GetDepartmentByName(string departmentName);

    Task Update(Product isAny);

    Task<Product?> GetById(Guid productId);
}

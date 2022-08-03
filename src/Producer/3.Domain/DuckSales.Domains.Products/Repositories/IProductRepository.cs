namespace DuckSales.Domains.Products.Repositories;

public interface IProductRepository : IRepository
{
    Task Add(Product product);

    Task<Departament?> GetDepartmentByName(string departmentName);

    Task Update(Product product);

    Task<Product?> GetById(Guid productId);
}

using DuckSales.Domains.Products.SeedWork;

namespace DuckSales.Infra.ProductsDataBase.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductsDBContext _context;
    private readonly DbSet<Product> _productSet;

    public ProductRepository(ProductsDBContext context)
    {
        _context = context;
        _productSet = _context.Set<Product>();
    }

    public async Task Add(Product product) => await _productSet.AddAsync(product);

    public Task<Departament?> GetDepartmentByName(string departmentName)
        => _context.Set<Departament>()
                   .FirstOrDefaultAsync(dept => dept.Name == departmentName);

    public Task Update(Product product)
    {
        _productSet.Update(product);
        return Task.CompletedTask;
    }

    public async Task<Product?> GetById(Guid productId) => await _productSet.FindAsync(productId);

    public IUnitOfWork UnitOfWork => _context;
}

using DuckSales.Domains.Products.SeedWork;

namespace DuckSales.Infra.ProductsDataBase;

public class ProductsDBContext : DbContext, IUnitOfWork
{
    public ProductsDBContext(DbContextOptions<ProductsDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfig());
        modelBuilder.ApplyConfiguration(new DepartmentConfig());
    }
}

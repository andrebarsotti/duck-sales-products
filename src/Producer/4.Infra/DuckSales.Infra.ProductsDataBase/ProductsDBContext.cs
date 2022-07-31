namespace DuckSales.Infra.ProductsDataBase;

public class ProductsDBContext : DbContext
{
    public ProductsDBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfig());
        modelBuilder.ApplyConfiguration(new DepartmentConfig());
    }
}

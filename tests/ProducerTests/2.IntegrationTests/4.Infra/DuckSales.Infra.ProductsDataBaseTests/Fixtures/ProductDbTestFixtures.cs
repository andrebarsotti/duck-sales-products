using Microsoft.Extensions.Configuration;

namespace DuckSales.Infra.ProductsDataBaseTests.Fixtures;

public sealed class ProductDbTestFixtures : IDisposable
{
    private readonly ProductsDBContext _dbContext;
    private bool _diposedValue;

    public ProductDbTestFixtures()
    {
        _dbContext = new ProductsDBContext(new DbContextOptionsBuilder<ProductsDBContext>()
            .UseSqlServer(Configs.Configuration.GetConnectionString("db"))
            .Options);
        _dbContext.Database.EnsureCreated();
    }

    public ProductsDBContext DbContext => _dbContext;

    public void Dispose() => Dispose(true);

    private void Dispose(bool disposing)
    {
        if (_diposedValue)
            return;

        if (disposing)
        {
            _dbContext.Dispose();
        }

        _diposedValue = true;
    }

    ~ProductDbTestFixtures() => Dispose(false);
}

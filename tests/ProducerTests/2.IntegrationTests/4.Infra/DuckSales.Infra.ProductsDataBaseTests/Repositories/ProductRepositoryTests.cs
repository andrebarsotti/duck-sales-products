using Bogus;
using DuckSales.Domains.ProductsTests.Entities.Fakers;
using FluentAssertions;

namespace DuckSales.Infra.ProductsDataBaseTests.Repositories;

public class ProductRepositoryTests : BaseTest, IClassFixture<ProductDbTestFixtures>
{
    private readonly ProductsDBContext _context;

    public ProductRepositoryTests(ProductDbTestFixtures fixtures)
    {
        _context = fixtures.DbContext;
        AutoMoqer.Use(_context);
    }

    [Fact]
    public async Task Add()
    {
        // Setup
        Product product = ProductFaker.GenerateWithOutIdAndNewDepartment();

        // Execute
        var repository = AutoMoqer.CreateInstance<ProductRepository>();
        await repository.Add(product);
        await repository.UnitOfWork.SaveChangesAsync();

        // Validate
        product.Id.Should().NotBe(default(Guid));
        product.Departament!.Id.Should().NotBe(default(Guid));
    }

    // TODO: Fazer os outros testes de integração.
}

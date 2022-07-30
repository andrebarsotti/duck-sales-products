using DuckSales.Domains.Products.Repositories;
using DuckSales.Domains.ProductsTests.Entities;
using Moq;

namespace DuckSales.Domains.ProductsTests.Services;

public class ProductsServiceTests : BaseTest
{
    [Fact]
    public async Task ProductsService_CreateANewProduct_Ok()
    {
        // Setup
        NewProductDto dto = new(Faker.Commerce.ProductName(),
                                Faker.Commerce.Department(max: 1),
                                Faker.Random.Int(min: 0),
                                Faker.Finance.Amount());

        Mock<IProductRepository> mockRepository = AutoMoqer.GetMock<IProductRepository>();

        Product? product = null;
        mockRepository.Setup(repo => repo.Add(It.IsAny<Product>()))
            .Callback<Product>(prd => product = prd);

        // Setup
        var service = AutoMoqer.CreateInstance<ProductService>();
        await service.CreateProduct(dto);

        // Validate
        mockRepository.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Once);
        product.Should().NotBeNull();
        product!.Name.Should().Be(dto.ProductName);
        product!.Departament?.Name.Should().Be(dto.DepartamentName);
        product!.QuantityAvaiableInStock.Should().Be(dto.QuantityAvaiableInStock);
        product!.UnitPrice.Should().Be(dto.UnitPrice);
    }

    [Fact]
    public async Task ProductsService_CreateANewProductOfAExistingDepartment_Ok()
    {
        // Setup
        Departament departament = new DepartmentFaker();

        NewProductDto dto = new(Faker.Commerce.ProductName(),
            departament.Name,
            Faker.Random.Int(min: 0),
            Faker.Finance.Amount());

        Mock<IProductRepository> mockRepository = AutoMoqer.GetMock<IProductRepository>();

        Product? product = null;
        mockRepository.Setup(repo => repo.Add(It.IsAny<Product>()))
            .Callback<Product>(prd => product = prd);

        mockRepository.Setup(repo => repo.GetDepartmentByName(It.IsAny<string>()))
                      .ReturnsAsync(departament);

        // Setup
        var service = AutoMoqer.CreateInstance<ProductService>();
        await service.CreateProduct(dto);

        // Validate
        mockRepository.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Once);
        mockRepository.Verify(repo => repo.GetDepartmentByName(dto.DepartamentName), Times.Once);

        product.Should().NotBeNull();
        product!.Name.Should().Be(dto.ProductName);
        product!.Departament.Should().BeSameAs(departament);
        product!.QuantityAvaiableInStock.Should().Be(dto.QuantityAvaiableInStock);
        product!.UnitPrice.Should().Be(dto.UnitPrice);
    }

    [Fact]
    public async Task ProductService_UpdateStockAndPriceOfProduct_Ok()
    {
        // Setup
        ProductUpdateDto dto = new(Faker.Random.Guid(),
                                   Faker.Random.Int(min: 0),
                                   Faker.Finance.Amount());

        Product product = new Faker<Product>()
            .RuleFor(prd => prd.Id, _ => dto.ProductId)
            .RuleFor(prd => prd.Name, fk => fk.Commerce.ProductName())
            .RuleFor(prd => prd.Departament, _ => new DepartmentFaker())
            .RuleFor(prd => prd.QuantityAvaiableInStock, fk => fk.Random.Int(min: 0))
            .RuleFor(prd => prd.UnitPrice, fk => fk.Finance.Amount());

        Mock<IProductRepository> mockRepository = AutoMoqer.GetMock<IProductRepository>();
        mockRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                      .ReturnsAsync(product);

        Guid productId = product.Id;
        string productName = product.Name;
        Departament? departament = product.Departament;
        int oldStock = product.QuantityAvaiableInStock;
        decimal oldUnitPrice = product.UnitPrice;

        // Execute
        var service = AutoMoqer.CreateInstance<ProductService>();
        await service.UpdateProduct(dto);

        // Validate
        mockRepository.Verify(repo => repo.Update(product), Times.Once());
        mockRepository.Verify(repo => repo.GetById(dto.ProductId), Times.Once);

        product.Id.Should().Be(productId);
        product.Name.Should().Be(productName);
        product.Departament.Should().BeSameAs(departament);
        product.QuantityAvaiableInStock.Should().NotBe(oldStock)
                                                .And.Be(dto.QuantityAvaiableInStock);
        product.UnitPrice.Should().NotBe(oldUnitPrice)
                                  .And.Be(dto.UnitPrice);
    }


    [Fact]
    public async Task ProductService_UpdateStockAndPriceOfUnexistedProduct_NOk()
    {
        // Setup
        ProductUpdateDto dto = new(Faker.Random.Guid(),
            Faker.Random.Int(min: 0),
            Faker.Finance.Amount());

        // Execute
        var service = AutoMoqer.CreateInstance<ProductService>();
        Func<Task> act = async () => { await service.UpdateProduct(dto); };

        // Validate
        await act.Should().ThrowAsync<ProductsDomainException>().WithMessage(ErrorMessages.ProductNotFound);
    }
}

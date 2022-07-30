namespace DuckSales.Domains.ProductsTests.Entities;

public class ProductTests : BaseTest
{
    [Fact]
    public void Product_UpdateName_Ok()
    {
        // Setup
        Product product = new();
        string newProductName = Faker.Commerce.ProductName();

        // Execute
        product.SetName(newProductName);

        // Validate
        product.Name.Should().Be(newProductName);
    }

    [Fact]
    public void Product_UpdateNameWithLessThen3Chars_NOk()
    {
        // Setup
        Product product = new();
        string newProductName = Faker.Random.String(1, 2);

        // Execute
        Action action = () => product.SetName(newProductName);

        // Validate
        action.Should().Throw<ProductsDomainException>().WithMessage(ErrorMessages.ProductNameWithInvalidSize);
    }

    [Fact]
    public void Product_UpdateNameWithMoreThen50Chars_NOk()
    {
        // Setup
        Product product = new();
        string newProductName = Faker.Random.String(51, 100);

        // Execute
        Action action = () => product.SetName(newProductName);

        // Validate
        action.Should().Throw<ProductsDomainException>().WithMessage(ErrorMessages.ProductNameWithInvalidSize);
    }

    [Fact]
    public void Product_SetANewDepartmentToProduct_Ok()
    {
        // Setup
        Product product = new();
        string departmentName = Faker.Commerce.Department(1);

        // Execute
        product.SetDepartment(departmentName);

        // Validate
        product.Departament.Should().NotBeNull();
        product.Departament!.Name.Should().Be(departmentName);
    }

    [Fact]
    public void Product_SetAExistingDepartmentToProduct_Ok()
    {
        // Setup
        Product product = new();
        Departament departament = new Faker<Departament>()
            .CustomInstantiator(fk => new Departament(fk.Commerce.Department(1)))
            .RuleFor(e => e.Id, fk => fk.Random.Guid());

        // Execute
        product.SetDepartment(departament);

        // Validate
        product.Departament.Should().BeSameAs(departament);
    }

    [Fact]
    public void Product_SetStockValue_Ok()
    {
        // Setup
        Product product = new();
        int stockValue = Faker.Random.Int(min: 0);

        // Execute
        product.SetQuantityAvaiableInStock(stockValue);

        // Validate
        product.QuantityAvaiableInStock.Should().Be(stockValue);
    }

    [Fact]
    public void Product_SetNegativeStockValue_NOk()
    {
        // Setup
        Product product = new();
        int stockValue = Faker.Random.Int(int.MinValue, -1);

        // Execute
        Action action = () => product.SetQuantityAvaiableInStock(stockValue);

        // Validate
        action.Should().Throw<ProductsDomainException>().WithMessage(ErrorMessages.NegativeStockNotAllowed);
    }

    [Fact]
    public void Product_SetUnitPrice_Ok()
    {
        // Setup
        Product product = new();
        decimal unitPrice = Faker.Finance.Amount();

        // Execute
        product.SetUnitPrice(unitPrice);

        // Validate
        product.UnitPrice.Should().Be(unitPrice);
    }

    [Fact]
    public void Product_SetNegativeUnitPrice_NOk()
    {
        // Setup
        Product product = new();
        decimal unitPrice = Faker.Random.Decimal(decimal.MinValue, decimal.MinusOne);

        // Execute
        Action action = () => product.SetUnitPrice(unitPrice);

        // Validate
        action.Should().Throw<ProductsDomainException>().WithMessage(ErrorMessages.NegativeUnitPriceNotAllowed);
    }
}

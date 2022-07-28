namespace DuckSales.Application.CommandsTests;

public class CreateProductCommandHandlerTests : BaseTest
{
    [Fact]
    public async Task Handle_CallCreateProductService_Ok()
    {
        // Setup
        CreateProductCommand command = new(Faker.Commerce.ProductName(),
            Faker.Commerce.Department(),
            Faker.Random.Int(0, 100),
            Faker.Finance.Amount(1m));

        Mock<IProductsService> mockService = AutoMoqer.GetMock<IProductsService>();

        NewProductDTO? productDto = null;
        mockService.Setup(service => service.CreateProduct(It.IsAny<NewProductDTO>()))
            .Callback<NewProductDTO>((dto) => productDto = dto)
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Execute
        var handler = AutoMoqer.CreateInstance<CreateProductCommandHandler>();
        var result = await handler.Handle(command, CancellationToken.None);

        // Validate
        result.Should().Be(Unit.Value);
        mockService.Verify();
        productDto.Should().BeEquivalentTo(command);
    }
}

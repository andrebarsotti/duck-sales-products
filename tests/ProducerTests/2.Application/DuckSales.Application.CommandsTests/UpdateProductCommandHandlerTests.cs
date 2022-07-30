namespace DuckSales.Application.CommandsTests;

public class UpdateProductCommandHandlerTests: BaseTest
{
    [Fact]
    public async Task Handle_CallUpateProductService_Ok()
    {
        // Setup
        UpdateProductCommand command = new(Faker.Random.Guid(), Faker.Random.Int(0, 100), Faker.Finance.Amount());

        Mock<IProductsService> mockService = AutoMoqer.GetMock<IProductsService>();

        ProductUpdateDto? productDto = null;
        mockService.Setup(service => service.UpdateProduct(It.IsAny<ProductUpdateDto>()))
            .Callback<ProductUpdateDto>((dto) => productDto = dto)
            .Returns(Task.CompletedTask)
            .Verifiable();


        // Execute
        var handler = AutoMoqer.CreateInstance<UpdateProductCommandHandler>();
        var result = await handler.Handle(command, CancellationToken.None);

        // Validate
        result.Should().Be(Unit.Value);
        mockService.Verify();
        productDto.Should().BeEquivalentTo(command);
    }
}

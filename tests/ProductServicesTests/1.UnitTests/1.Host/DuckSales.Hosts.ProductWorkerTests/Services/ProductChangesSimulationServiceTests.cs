namespace DuckSales.Hosts.ProductWorkerTests.Services;

public class ProductChangesSimulationServiceTests : BaseTest
{
    [Fact]
    public async Task Execute_CreateANewProduct_Ok()
    {
        // Setup
        ProductWorkerSettings settings =
            new ProductWorkerSettings { MaximumNumberOfProducts = Faker.Random.Int(1, 10) };

        Mock<IOptions<ProductWorkerSettings>>
            mockProductSettings = AutoMoqer.GetMock<IOptions<ProductWorkerSettings>>();
        mockProductSettings.SetupGet(opt => opt.Value)
            .Returns(settings)
            .Verifiable();

        Mock<IMediator> mockMediator = AutoMoqer.GetMock<IMediator>();

        mockMediator.Setup(meditor =>
                meditor.Send(It.IsAny<GetTotalAmountOfProdcuntFromCatalogQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0)
            .Verifiable();

        CreateProductCommand? command = null;
        mockMediator.Setup(meditor => meditor.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
            .Callback<IRequest<Unit>, CancellationToken>(
                (cmd, _) => command = (CreateProductCommand)cmd
            )
            .Returns(Unit.Task)
            .Verifiable();

        // Execute
        await ExecuteService();

        // Validate
        mockMediator.Verify();
        command.Should().NotBeNull();
        command!.ProductName.Should().NotBeNullOrWhiteSpace();
        command!.DepartamentName.Should().NotBeNullOrWhiteSpace();
        command!.QuantityAvaiableInStock.Should().BePositive();
        command!.UnitPrice.Should().BePositive();
    }

    private Task ExecuteService()
        => AutoMoqer.CreateInstance<ProductChangesSimulationService>().Execute(CancellationToken.None);

    [Fact]
    public async Task Execute_OnlyUpdateProductWhenTheMaximumNumberOfProductIsReach_Ok()
    {
        // Setup
        ProductWorkerSettings settings =
            new ProductWorkerSettings { MaximumNumberOfProducts = Faker.Random.Int(1, 10) };

        Mock<IOptions<ProductWorkerSettings>>
            mockProductSettings = AutoMoqer.GetMock<IOptions<ProductWorkerSettings>>();
        mockProductSettings.SetupGet(opt => opt.Value)
            .Returns(settings)
            .Verifiable();

        Mock<IMediator> mockMediator = AutoMoqer.GetMock<IMediator>();

        mockMediator.Setup(meditor =>
                meditor.Send(It.IsAny<GetTotalAmountOfProdcuntFromCatalogQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(settings.MaximumNumberOfProducts)
            .Verifiable();

        List<ProductOfCatalogReadModel> listOfProduct = new Faker<ProductOfCatalogReadModel>()
            .CustomInstantiator(f => new ProductOfCatalogReadModel(
                f.Random.Guid(),
                f.Commerce.ProductName(),
                f.Commerce.Department(),
                f.Random.Int(0, 100),
                f.Finance.Amount(1m)
            )).Generate(settings.MaximumNumberOfProducts);
        mockMediator.Setup(meditor =>
                meditor.Send(It.IsAny<GetAllProductsFromCatalogQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(listOfProduct)
            .Verifiable();

        UpdateProductCommand? command = null;
        mockMediator.Setup(meditor => meditor.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
            .Callback<IRequest<Unit>, CancellationToken>(
                (cmd, _) => command = (UpdateProductCommand)cmd
            )
            .Returns(Unit.Task)
            .Verifiable();

        // Execute
        await ExecuteService();

        //Validate
        mockProductSettings.Verify();
        mockMediator.Verify();
        mockMediator.VerifyNoOtherCalls();

        command.Should().NotBeNull();
        listOfProduct.Select(prd => prd.ProductId).Should().Contain(command!.ProductId);
        command!.QuantityAvaiableInStock.Should().BePositive();
        command!.UnitPrice.Should().BePositive();
    }
}

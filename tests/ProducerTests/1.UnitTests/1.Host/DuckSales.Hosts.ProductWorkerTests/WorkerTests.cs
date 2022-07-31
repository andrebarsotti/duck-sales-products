using Microsoft.Extensions.DependencyInjection;

namespace DuckSales.Hosts.ProductWorkerTests;

public class WorkerTests : BaseTest
{
    public WorkerTests() : base() { }

    [Fact]
    public async Task ExecuteAsync_ExucuteScopedProductSevice_Ok()
    {
        // Setup
        Mock<IProductChangesSimulationService> mockProductsService = AutoMoqer
                                                                        .GetMock<IProductChangesSimulationService>();
        mockProductsService.Setup(service => service.Execute())
            .Returns(async Task () => await Task.Delay(100))
            .Verifiable();

        Mock<IServiceScopeFactory> mockServiceScopeFactory = AutoMoqer.GetMock<IServiceScopeFactory>();
        Mock<IServiceProvider> mockServiceProvider = AutoMoqer.GetMock<IServiceProvider>();
        Mock<IServiceScope> mockServiceScope = AutoMoqer.GetMock<IServiceScope>();

        mockServiceScope.SetupGet(x => x.ServiceProvider)
            .Returns(() => mockServiceProvider.Object)
            .Verifiable();

        mockServiceScopeFactory.Setup(factory => factory.CreateScope())
            .Returns(() => mockServiceScope.Object)
            .Verifiable();

        mockServiceProvider.Setup(service => service.GetService(typeof(IServiceScopeFactory)))
            .Returns<Type>(_ => mockServiceScopeFactory.Object)
            .Verifiable();

        mockServiceProvider.Setup(service => service.GetService(typeof(IProductChangesSimulationService)))
            .Returns<Type>(_ => mockProductsService.Object)
            .Verifiable();

        // Execute
        var worker = AutoMoqer.CreateInstance<Worker>();
        await worker.StartAsync(CancellationToken.None);
        await worker.StopAsync(CancellationToken.None);

        // Validate
        worker.ExecuteTask.IsCompleted.Should().BeTrue();
        mockServiceProvider.Verify();
        mockServiceScopeFactory.Verify();
        mockServiceScope.Verify();
        mockProductsService.Verify();
    }
}

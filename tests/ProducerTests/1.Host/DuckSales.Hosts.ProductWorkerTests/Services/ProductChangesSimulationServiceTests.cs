using MediatR;

namespace DuckSales.Hosts.ProductWorkerTests.Services;

public class ProductChangesSimulationServiceTests : BaseTest
{
    public ProductChangesSimulationServiceTests() : base() { }

    [Fact]
    public async Task Execute()
    {
        // Setup
        // TODO: Recuperar a configuração do número máximo de produtos
        // TODO: Consultar o banco de dados para verificar se a quantidade bate com o parâmetro.
        // TODO: Consultar os produtos existentes no banco.
        // TODO: Atualizar um produto.
        CreateProductCommand? command = null;
        Mock<IMediator> mockMediator = AutoMoqer.GetMock<IMediator>();
        mockMediator.Setup(meditor => meditor.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
            .Callback<IRequest<Unit>, CancellationToken>(
                    (cmd, _) => command = (CreateProductCommand)cmd
                )
            .Returns(Unit.Task)
            .Verifiable();

        // Execute
        var service = AutoMoqer.CreateInstance<ProductChangesSimulationService>();
        await service.Execute();

        // Validate
        mockMediator.Verify();
        command.Should().NotBeNull();
        command?.ProductName.Should().NotBeNullOrWhiteSpace();
        command?.DepartamentName.Should().NotBeNullOrWhiteSpace();
        command?.QuantityAvaiableInStock.Should().NotBeNull().And.BePositive();
        command?.UnitPrice.Should().NotBeNull().And.BePositive();
    }
}

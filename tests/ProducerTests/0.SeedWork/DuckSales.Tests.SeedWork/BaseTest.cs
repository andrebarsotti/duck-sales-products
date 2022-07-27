namespace DuckSales.Tests.SeedWork;
public abstract class BaseTest
{
    private readonly AutoMocker _autoMoq;
    private readonly Faker _faker;

    protected BaseTest()
        => (_autoMoq, _faker) = (new AutoMocker(), new Faker());

    protected AutoMocker AutoMoqer => _autoMoq;

    protected Faker Faker => _faker;
}

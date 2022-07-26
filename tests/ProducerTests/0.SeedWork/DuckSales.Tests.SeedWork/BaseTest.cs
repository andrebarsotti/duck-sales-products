namespace DuckSales.Tests.SeedWork;
public abstract class BaseTest
{
    private readonly AutoMoqer _autoMoq;
    private readonly Faker _faker;

    protected BaseTest()
    {
        _autoMoq = new AutoMoqer();
        _faker = new Faker();
    }

    protected AutoMoqer AutoMoqer => _autoMoq;

    protected Faker Faker => _faker;
}

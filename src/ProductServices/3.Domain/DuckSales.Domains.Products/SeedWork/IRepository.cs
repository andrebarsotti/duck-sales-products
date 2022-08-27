namespace DuckSales.Domains.Products.SeedWork;

public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }
}

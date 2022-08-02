using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DuckSales.Infra.ProductsDataBase.SeedWork;

public class DateTimeUtcNowValueGenerator : ValueGenerator<DateTime>
{
    public override DateTime Next(EntityEntry entry) => DateTime.UtcNow;

    public override bool GeneratesTemporaryValues => false;
}

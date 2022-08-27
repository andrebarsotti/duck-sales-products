using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DuckSales.Infra.ProductsDataBase.SeedWork;

public class UpdateDateTimeUtcNowValuGenerator : ValueGenerator<DateTime?>
{
    public override DateTime? Next(EntityEntry entry)
        => entry.State == EntityState.Modified ? DateTime.UtcNow : null;

    public override bool GeneratesTemporaryValues => false;
}

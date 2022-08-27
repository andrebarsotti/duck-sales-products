namespace DuckSales.Hosts.ProductWorker;

public class ProductWorkerSettings
{
  public int MaximumNumberOfProducts { get; init; }

  public int? WaitingTimeBeferoNextCall { get; init; }
}

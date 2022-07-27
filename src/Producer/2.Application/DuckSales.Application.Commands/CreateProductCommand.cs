namespace DuckSales.Application.Commands;
public class CreateProductCommand : IRequest
{
    public string? ProductName { get; init; }

    public string? DepartamentName { get; init; }

    public int? QuantityAvaiableInStock { get; init; }

    public decimal? UnitPrice { get; init; }
}

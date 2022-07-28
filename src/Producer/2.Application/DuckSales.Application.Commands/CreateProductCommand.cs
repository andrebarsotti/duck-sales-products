namespace DuckSales.Application.Commands;

public record CreateProductCommand(string ProductName,
                                   string DepartamentName,
                                   int QuantityAvaiableInStock,
                                   decimal UnitPrice) : IRequest;

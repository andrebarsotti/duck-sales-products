namespace DuckSales.Application.Commands;

public record UpdateProductCommand(Guid ProductId,
                                   int QuantityAvaiableInStock,
                                   decimal UnitPrice): IRequest;

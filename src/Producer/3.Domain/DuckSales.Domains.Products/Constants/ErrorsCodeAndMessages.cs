namespace DuckSales.Domains.Products.Constants;

public static class ErrorMessages
{
    public static readonly string ProductNameWithInvalidSize = $"The product name must have at least {Product.MinNameSize} and no more then {Product.MaxNameSize} chars";
    public const string NegativeStockNotAllowed = "The stock value must be greater or equal zero.";
    public const string NegativeUnitPriceNotAllowed = "The unit price must be greater or equal zero.";
    public static readonly string DepartmentNameWithInvalidSize = $"The product name must have at least {Departament.MinNameSize} and no more then {Departament.MaxNameSize} chars";
}

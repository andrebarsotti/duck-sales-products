namespace DuckSales.Domains.Products.Entities;

public class Product : Entity
{
    private string _name;
    private Departament? _department;
    private int _quantityAvaiableInStock;
    private decimal _unitprice;

    internal const int MinNameSize = 3;
    internal const int MaxNameSize = 50;

    public Product()
    {
        _name = string.Empty;
        _department = null;
        _quantityAvaiableInStock = 0;
        _unitprice = decimal.Zero;
    }

    public string Name => _name;

    public Departament? Departament => _department;

    public int QuantityAvaiableInStock => _quantityAvaiableInStock;

    public decimal UnitPrice => _unitprice;

    public void SetName(string productName)
    {
        if (productName.Length is < MinNameSize or > MaxNameSize)
            throw new ProductsDomainException(ErrorMessages.ProductNameWithInvalidSize);

        _name = productName;
    }

    public void SetDepartment(string departmentName) => _department = new Departament(departmentName);

    public void SetDepartment(Departament department) => _department = department;

    public void SetQuantityAvaiableInStock(int stockValue)
    {
        if (stockValue < 0)
            throw new ProductsDomainException(ErrorMessages.NegativeStockNotAllowed);

        _quantityAvaiableInStock = stockValue;
    }

    public void SetUnitPrice(decimal unitPrice)
    {
        if (unitPrice < decimal.Zero)
            throw new ProductsDomainException(ErrorMessages.NegativeUnitPriceNotAllowed);

        _unitprice = unitPrice;
    }
}

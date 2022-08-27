using System.Diagnostics;

namespace DuckSales.Domains.Products.Entities;

public class Departament : Entity
{
    private string? _name;

    public const int MinNameSize = 3;
    public const int MaxNameSize = 20;

    public Departament(string name)
    {
        Name = name;
    }

    public string Name
    {
        get
        {
            return _name!;
        }
        protected set
        {
            if (value.Length is < MinNameSize or > MaxNameSize)
                throw new ProductsDomainException(ErrorMessages.DepartmentNameWithInvalidSize);

            _name = value;
        }
    }
}

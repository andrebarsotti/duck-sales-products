namespace DuckSales.Domains.ProductsTests.Entities;

public sealed class DepartmentFaker : Faker<Departament>
{
    public DepartmentFaker()
    {
        CustomInstantiator(fk => new Departament(fk.Commerce.Department(1)));
        RuleFor(e => e.Id, fk => fk.Random.Guid());
    }
}

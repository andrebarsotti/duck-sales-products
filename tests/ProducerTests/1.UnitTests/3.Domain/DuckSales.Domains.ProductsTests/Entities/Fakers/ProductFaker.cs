namespace DuckSales.Domains.ProductsTests.Entities.Fakers;

public static class ProductFaker
{
    private static Faker _faker = new();

    public static Product Generate()
        => FillAllProductProperties(new Faker<Product>()
            .RuleFor(prd => prd.Id, fk => fk.Random.Guid()));

    public static Product GenerateWithOutId() => FillAllProductProperties(new Product());

    public static Product GenerateWithOutIdAndNewDepartment()
        => FillAllProductProperties(new Product(), new Departament(_faker.Commerce.Department(max: 1)));

    private static Product FillAllProductProperties(Product product)
        => FillAllProductProperties(product, new DepartmentFaker());

    private static Product FillAllProductProperties(Product product, Departament departament)
    {
        product.SetName(_faker.Commerce.ProductName());
        product.SetDepartment(departament);
        product.SetQuantityAvaiableInStock(_faker.Random.Int(min: 0));
        product.SetUnitPrice(_faker.Finance.Amount());
        return product;
    }

}

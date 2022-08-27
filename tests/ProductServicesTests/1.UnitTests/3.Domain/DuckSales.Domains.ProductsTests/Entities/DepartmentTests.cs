namespace DuckSales.Domains.ProductsTests.Entities;

public class DepartmentTests : BaseTest
{

    [Fact]
    public void Department_CreateDepartment_OK()
    {
        // Setup
        string departmentName = Faker.Commerce.Department(max: 1);

        // Execute
        Departament departament = new(departmentName);

        // Validate
        departament.Name.Should().Be(departmentName);
    }

   [Fact]
   public void Department_SetNameWithLessThen3Chars_NOK()
   {
       // Setup
       string departName = Faker.Random.String(minLength: 1, maxLength: 2);

       // Execute
       Action action = () => new Departament(departName);

       // Validate
       action.Should().Throw<ProductsDomainException>().WithMessage(ErrorMessages.DepartmentNameWithInvalidSize);
   }

  [Fact]
   public void Department_SetNameWithLessThen20Chars_NOK()
   {
       // Setup
       string departName = Faker.Random.String(minLength: 20, maxLength: 100);

       // Execute
       Action action = () => new Departament(departName);

       // Validate
       action.Should().Throw<ProductsDomainException>().WithMessage(ErrorMessages.DepartmentNameWithInvalidSize);
   }
}

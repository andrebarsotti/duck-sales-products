namespace DuckSales.Domains.ProductsTests.Entities;

public class DepartmentTests : BaseTest
{
   [Fact]
   public void Department_SetNameWithLessThen3Chars_NOK()
   {
       // Setup
       string departName = Faker.Random.String(minLength: 1, maxLength: 2);

       // Execute
       Action action = () =>
       {
           var department = new Departament(departName);
       };

       // Validate
       action.Should().Throw<ProductsDomainException>().WithMessage(ErrorMessages.DepartmentNameWithInvalidSize);
   }

  [Fact]
   public void Department_SetNameWithLessThen20Chars_NOK()
   {
       // Setup
       string departName = Faker.Random.String(minLength: 20, maxLength: 100);

       // Execute
       Action action = () =>
       {
           var department = new Departament(departName);
       };

       // Validate
       action.Should().Throw<ProductsDomainException>().WithMessage(ErrorMessages.DepartmentNameWithInvalidSize);
   }
}

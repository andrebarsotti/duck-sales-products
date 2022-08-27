using DuckSales.Infra.ProductsDataBase.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DuckSales.Infra.ProductsDataBase.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<ProductsDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(DatabaseConstants.DataBaseConnection)));
        service.AddScoped<IProductRepository, ProductRepository>();
        return service;
    }
}

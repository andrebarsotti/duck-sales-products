using DuckSales.Domains.Products.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DuckSales.Domains.Products.Extensions;

public static class ServiceExtensions
{
    public static void AddDomainServices(this IServiceCollection service)
        => service.AddScoped<IProductsService, ProductService>();
}

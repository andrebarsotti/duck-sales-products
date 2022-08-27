using Microsoft.Extensions.DependencyInjection;

namespace DuckSales.Application.QueryHandlers.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddQueries(this IServiceCollection service)
        => service.AddMediatR(typeof(ServiceExtensions).Assembly);

}

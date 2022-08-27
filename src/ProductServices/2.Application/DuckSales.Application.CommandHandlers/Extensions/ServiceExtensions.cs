using Microsoft.Extensions.DependencyInjection;

namespace DuckSales.Application.CommandHandlers.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCommannds(this IServiceCollection service)
        => service.AddMediatR(typeof(ServiceExtensions).Assembly);

}

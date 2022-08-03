using DuckSales.Hosts.ProductWorker.Extensions;
using MediatR;

namespace DuckSales.Hosts.ProductWorker.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            _logger.LogInformation("----- Handling command/query {CommandName} ({@Command})", request.GetGenericTypeName(), request);
            var response = await next();
            _logger.LogInformation("----- Command/quey {CommandName} handled - response: {@Response}", request.GetGenericTypeName(), response);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Hanlding {CommandName}", request.GetGenericTypeName());
            throw;
        }
    }
}

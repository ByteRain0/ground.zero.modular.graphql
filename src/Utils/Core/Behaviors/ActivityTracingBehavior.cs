using Core.Otel;
using MediatR;

namespace Core.Behaviors;

/// <summary>
/// As a PoC wrap all mediatr requests in a span.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ActivityTracingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    { 
        using var activity = RunTimeDiagnosticConfig.Source.StartActivity(typeof(TRequest).Name);
        return await next(); 
    }
}
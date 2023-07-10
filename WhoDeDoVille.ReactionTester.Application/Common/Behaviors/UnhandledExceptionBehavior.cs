namespace WhoDeDoVille.ReactionTester.Application.Common.Behaviors;

/// <summary>
///     Unhandled Exception Behavior.
/// </summary>
public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<IRequest<TResponse>, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(IRequest<TResponse> request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "User Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}
using Newtonsoft.Json;
using WhoDeDoVille.ReactionTester.Logging;

namespace WhoDeDoVille.ReactionTester.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly LoggingMessages _loggingMessages;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
        _loggingMessages = new LoggingMessages(_logger);
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _loggingMessages.LoggingBehaviorHandling(typeof(TRequest).Name);
        _loggingMessages.LoggingBehaviorHandlingWithParams(typeof(TRequest).Name, request.GetType().GetProperties().ToString(), JsonConvert.SerializeObject(request));

        var response = await next();

        _loggingMessages.LoggingBehaviorHandled(typeof(TRequest).Name, typeof(TResponse).Name);
        _loggingMessages.LoggingBehaviorHandledWithResponse(typeof(TRequest).Name, typeof(TResponse).Name, JsonConvert.SerializeObject(response));

        return response;
    }
}

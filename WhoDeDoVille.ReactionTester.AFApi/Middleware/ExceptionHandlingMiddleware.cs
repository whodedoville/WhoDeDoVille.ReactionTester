using Microsoft.Azure.Functions.Worker.Middleware;
using WhoDeDoVille.ReactionTester.Application.Common.Error;
using WhoDeDoVille.ReactionTester.Application.Common.Exceptions;
using WhoDeDoVille.ReactionTester.Infrastructure.Common;
using ApplicationException = WhoDeDoVille.ReactionTester.Domain.Exceptions.ApplicationException;

namespace WhoDeDoVille.ReactionTester.AFApi.Middleware;

internal sealed class ExceptionHandlingMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly LoggingExceptionMessages _loggingExceptionMessages;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
        _loggingExceptionMessages = new LoggingExceptionMessages(_logger);
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (AggregateException ex)
        {
            foreach (Exception innerException in ex.InnerExceptions)
            {
                HandleLoggingException(innerException);
            }
            if (ex.InnerExceptions.Count > 0)
            {
                await HandleExceptionAsync(context, ex.InnerExceptions[0]);
            }
        }
    }

    private static async Task HandleExceptionAsync(FunctionContext context, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        //var response = new
        var response = new ErrorResponse()
        {
            Title = GetTitle(exception),
            Status = statusCode,
            Detail = exception.Message,
            CreatedAt = DateTimeProvider.UtcNow,
            Errors = GetErrors(exception)
        };

        var httpReqData = await context.GetHttpRequestDataAsync();

        if (httpReqData != null)
        {
            var newHttpResponse = httpReqData.CreateResponse(statusCode);

            await newHttpResponse.WriteAsJsonAsync(response, newHttpResponse.StatusCode);

            var invocationResult = context.GetInvocationResult();

            var httpOutputBindingFromMultipleOutputBindings = GetHttpOutputBindingFromMultipleOutputBinding(context);
            if (httpOutputBindingFromMultipleOutputBindings is not null)
            {
                httpOutputBindingFromMultipleOutputBindings.Value = newHttpResponse;
            }
            else
            {
                invocationResult.Value = newHttpResponse;
            }
        }
    }
    private static HttpStatusCode GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => HttpStatusCode.BadRequest,
            EntityNotFoundException => HttpStatusCode.NotFound,
            NotFoundException => HttpStatusCode.NotFound,
            ValidationException => HttpStatusCode.UnprocessableEntity,
            _ => HttpStatusCode.InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };

    private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
    {
        IReadOnlyDictionary<string, string[]> errors = null;

        if (exception is ValidationException validationException)
        {
            errors = validationException.ErrorsDictionary;
        }

        return errors;
    }

    private static OutputBindingData<HttpResponseData> GetHttpOutputBindingFromMultipleOutputBinding(FunctionContext context)
    {
        // The output binding entry name will be "$return" only when the function return type is HttpResponseData
        var httpOutputBinding = context.GetOutputBindings<HttpResponseData>()
            .FirstOrDefault(b => b.BindingType == "http" && b.Name != "$return");

        return httpOutputBinding;
    }

    private void HandleLoggingException(Exception ex)
    {
        var exName = ex.GetType().Name;
        var exMsg = ex.Message;

        int exSC = (int)GetStatusCode(ex);
        var scOnes = exSC.ToString().Substring(0, 1);
        var scTens = exSC.ToString().Substring(0, 2);
        var scHundreds = exSC.ToString();

        switch (scHundreds)
        {
            default:
                switch (scTens)
                {
                    default:
                        switch (scOnes)
                        {
                            case "1":
                                _loggingExceptionMessages.LogExceptionHttpStatusCode100s(ex, exSC, exName, exMsg);
                                break;
                            case "2":
                                _loggingExceptionMessages.LogExceptionHttpStatusCode200s(ex, exSC, exName, exMsg);
                                break;
                            case "3":
                                _loggingExceptionMessages.LogExceptionHttpStatusCode300s(ex, exSC, exName, exMsg);
                                break;
                            case "4":
                                _loggingExceptionMessages.LogExceptionHttpStatusCode400s(ex, exSC, exName, exMsg);
                                break;
                            default:
                                _loggingExceptionMessages.LogExceptionHttpStatusCode500s(ex, exSC, exName, exMsg);
                                break;
                        }
                        break;
                }
                break;
        }
    }
}

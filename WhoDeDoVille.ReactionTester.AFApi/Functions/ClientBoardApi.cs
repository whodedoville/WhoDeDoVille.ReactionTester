namespace WhoDeDoVille.ReactionTester.AFApi.Functions;

public class ClientBoardApi
{
    //private readonly ILogger _logger;
    private readonly ISender _sender;
    private readonly LoggingMessages _loggingMessages;

    //public ClientBoardApi(ILoggerFactory loggerFactory, ISender sender)
    //{
    //    _logger = loggerFactory.CreateLogger<ClientBoardApi>();
    //    _sender = sender;
    //    _loggingMessages = new LoggingMessages(_logger);
    //}

    public ClientBoardApi(ILogger<ClientBoardApi> logger, ISender sender)
    {
        //_logger = loggerFactory.CreateLogger<ClientBoardApi>();
        _sender = sender;
        _loggingMessages = new LoggingMessages(logger);
    }

    [Function("GetBoardApi")]
    public HttpResponseData RunGetBoardApi([HttpTrigger(AuthorizationLevel.Function, "get", Route = "getBoard/{difficultyLevel:int}")] HttpRequestData req, int difficultyLevel)
    {
        _loggingMessages.AzureFunctionRequest(this.GetType().Name);

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString("Welcome to Azure Functions!");
        _loggingMessages.AzureFunctionResponse(this.GetType().Name, response.GetType().Name);
        return response;
    }
}

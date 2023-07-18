namespace WhoDeDoVille.ReactionTester.AFApi.Functions;

public class GenerateBoardApi
{
    private readonly ILogger _logger;
    private readonly ISender _sender;
    private readonly LoggingMessages _loggingMessages;

    public GenerateBoardApi(ILoggerFactory loggerFactory, ISender sender)
    {
        _logger = loggerFactory.CreateLogger<GenerateBoardApi>();
        _sender = sender;
        _loggingMessages = new LoggingMessages(_logger);
    }

    /// <summary>
    ///     Generates game boards and inserts them into the database.
    /// </summary>
    /// <param name="req">Http Request Data</param>
    /// <param name="difficultyLevel">Board Difficulty Level</param>
    /// <param name="sequenceNum">
    ///     Needs to be a unique number. Supposed to be in sequence by difficulty
    /// </param>
    /// <param name="boardCount">Minimum amount of boards to generate</param>
    /// <returns>Boolean if boards were successfully created</returns>
    [Function("GenerateBoardApi")]
    public async Task<HttpResponseData> RunGenerateBoard([HttpTrigger(AuthorizationLevel.Function, "get", Route = "generateboard/{difficultyLevel:int}/{sequenceNumber:int}/{boardCount:int}")] HttpRequestData req,
        int difficultyLevel,
        string sequenceNumber,
        int boardCount)
    {
        _loggingMessages.AzureFunctionRequest(this.GetType().Name);

        var responseData = await _sender.Send(new GenerateBoardCommand
        {
            DifficultyLevel = difficultyLevel,
            SequenceNumber = sequenceNumber,
            BoardCount = boardCount
        });

        if (responseData == null)
        {
            var msg = "No board available.";
            _loggingMessages.AzureFunctionMethodErrorMessage(this.GetType().Name, msg);
            throw new BadRequestException(msg);
        }

        var response = req.CreateResponse();
        await response.WriteAsJsonAsync(responseData);
        _loggingMessages.AzureFunctionResponse(this.GetType().Name, responseData.GetType().Name);
        return response;
    }

    [Function("GenerateBoardByDifficultyApi")]
    public async Task<HttpResponseData> RunGenerateBoardByDifficulty([HttpTrigger(AuthorizationLevel.Function, "get", Route = "generateboardbydifficulty/{difficultyLevel:int}/{boardCount:int?}")] HttpRequestData req,
        int difficultyLevel,
        int? boardCount)
    {
        _loggingMessages.AzureFunctionRequest(this.GetType().Name);

        var responseData = await _sender.Send(new GenerateBoardGetNextSequenceCommand
        {
            DifficultyLevel = difficultyLevel,
            BoardCount = boardCount
        });

        if (responseData == false)
        {
            var msg = "No board generated.";
            _loggingMessages.AzureFunctionMethodErrorMessage(this.GetType().Name, msg);
            throw new BadRequestException(msg);
        }

        var response = req.CreateResponse();
        await response.WriteAsJsonAsync(responseData);
        _loggingMessages.AzureFunctionResponse(this.GetType().Name, responseData.GetType().Name);
        return response;
    }
}

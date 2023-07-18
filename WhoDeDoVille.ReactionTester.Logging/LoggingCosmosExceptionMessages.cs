namespace WhoDeDoVille.ReactionTester.Logging;
public partial class LoggingCosmosExceptionMessages
{
    private readonly ILogger _logger;

    public LoggingCosmosExceptionMessages(ILogger logger)
    {
        _logger = logger;
    }

    [LoggerMessage(
        EventId = (int)LoggingEventId.CosmosAddItemAsync,
        Level = LogLevel.Error,
        Message = "CosmosAddItemAsync Item:{item}")]
    public partial void LogCosmosExceptionAddItemAsync(Exception ex, string item);
}

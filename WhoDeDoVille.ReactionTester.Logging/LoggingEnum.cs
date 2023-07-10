public enum LoggingEventId
{
    /// <summary>
    /// Azure Functions Http Trigger Request.
    /// </summary>
    AzureFunctionsHttpTriggerRequest = 1,
    AzureFunctionsHttpTriggerResponse = 2,
    AzureFunctionsHttpTriggerRequestException = 5,
    AzureFunctionsHttpTriggerRequestErrorMessage = 9,
    LoggingBehaviorHandling = 20,
    LoggingBehaviorHandled = 25,
    LogEmptyReturnVariable = 50,
    LogEmptyReturnVariableWithParameters = 51,
    DatabaseCreationFailed = 60,
    ContainerCreationFailed = 61,
    ExceptionHttpStatusCode100s = 101,
    ExceptionHttpStatusCode200s = 102,
    ExceptionHttpStatusCode300s = 103,
    ExceptionHttpStatusCode400s = 104,
    ExceptionHttpStatusCode500s = 105,

}
namespace WhoDeDoVille.ReactionTester.AFApi.Functions
{
    public class UserApi
    {
        private readonly ILogger _logger;
        private readonly ISender _sender;
        private readonly LoggingMessages _loggingMessages;

        public UserApi(
            ILoggerFactory loggerFactory,
            ISender sender
        )
        {
            _logger = loggerFactory.CreateLogger<UserApi>();
            _sender = sender;
            _loggingMessages = new LoggingMessages(_logger);
        }

        [Function("AddUser")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "adduser")] HttpRequestData req)
        {
            _loggingMessages.AzureFunctionRequest(this.GetType().Name);

            var requestParameters = await RequestParameterProvider.ReturnReqParameters(req);

            var response = req.CreateResponse();

            var responseData = await _sender.Send(new AddUserCommand
            {
                Email = requestParameters["Email"],
                Username = requestParameters["Username"]
            });

            await response.WriteAsJsonAsync(responseData);
            _loggingMessages.AzureFunctionResponse(this.GetType().Name, responseData.GetType().Name);
            return response;
        }
    }
}

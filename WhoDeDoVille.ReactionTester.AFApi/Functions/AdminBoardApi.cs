using Svg;
using WhoDeDoVille.ReactionTester.Application.SVG.Queries;

namespace WhoDeDoVille.ReactionTester.AFApi.Functions
{
    public class AdminBoardApi
    {
        private readonly ILogger _logger;
        private readonly ISender _sender;
        private readonly LoggingMessages _loggingMessages;

        public AdminBoardApi(ILoggerFactory loggerFactory, ISender sender)
        {
            _logger = loggerFactory.CreateLogger<AdminBoardApi>();
            _sender = sender;
            _loggingMessages = new LoggingMessages(_logger);
        }
        /// <summary>
        /// Gets Board ID and colors.
        /// </summary>
        /// <param name="req">Http Request Data</param>
        /// <param name="boardId">Board Id. eg: 3:3:hash ... Answer.Count:Shapes.Count:Hash</param>
        /// <param name="color1">User Hex color Color1. No # at beginning.</param>
        /// <param name="color2">User Hex color Color2. No # at beginning.</param>
        /// <param name="color3">User Hex color Color3. No # at beginning.</param>
        /// <returns>Svg XML</returns>
        [Function("GetSvgByBoardIdAndColorsApi")]
        public async Task<HttpResponseData> RunGetSvgByBoardIdAndColors([HttpTrigger(AuthorizationLevel.Function,
            "get",
            Route = "getSvgByBoardIdAndColors/{boardId}/{color1}/{color2}/{color3}")] HttpRequestData req,
            string boardId, string color1, string color2, string color3)
        {
            _loggingMessages.AzureFunctionRequest(this.GetType().Name);

            var resSvgDocument = await _sender.Send(new GetSingleSvgBoardByBoardIdAndColorsQuery
            {
                BoardId = boardId,
                Color1 = color1,
                Color2 = color2,
                Color3 = color3
            });

            if (resSvgDocument == null)
            {
                var msg = "No board available.";
                _loggingMessages.AzureFunctionMethodErrorMessage(this.GetType().Name, msg);
                throw new BadRequestException(msg);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString(resSvgDocument.GetXML());
            _loggingMessages.AzureFunctionResponse(this.GetType().Name, resSvgDocument.GetType().Name);
            return response;
        }

        //[Function("DeleteBoardByDifficultyApi")]
        //public async Task<HttpResponseData> RunDeleteBoardByDifficulty([HttpTrigger(AuthorizationLevel.Function, "get", Route = "deleteBoardByDifficulty/{difficultyLevel:int}")] HttpRequestData req, int difficultyLevel)
        //{
        //    _logger.LogInformation("C# HTTP trigger DeleteBoardByDifficultyApi function processed a request.");

        //    var response = req.CreateResponse(HttpStatusCode.OK);
        //    response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        //    var resSvgDocument = await _sender.Send(new GetSingleSvgBoardByBoardIdAndColorsQuery
        //    {
        //        BoardId = boardId,
        //        Color1 = color1,
        //        Color2 = color2,
        //        Color3 = color3
        //    });

        //    response.WriteString(resSvgDocument.GetXML());

        //    return response;
        //}
    }
}

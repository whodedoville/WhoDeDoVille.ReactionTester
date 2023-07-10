using Svg;
using WhoDeDoVille.ReactionTester.Logging;

namespace WhoDeDoVille.ReactionTester.Application.SVG.Queries;

public class GetSingleSvgBoardByBoardIdAndColorsQuery : IQuery<SvgDocument>
{
    public string BoardId { get; set; }
    public string Color1 { get; set; }
    public string Color2 { get; set; }
    public string Color3 { get; set; }

    public class GetSingleSvgBoardByBoardIdAndColorsHandler : ApplicationBase, IRequestHandler<GetSingleSvgBoardByBoardIdAndColorsQuery, SvgDocument>
    {
        private readonly ILogger _logger;
        private readonly LoggingMessages _loggingMessages;

        public GetSingleSvgBoardByBoardIdAndColorsHandler(IBoardRepository boardRepository, IMapper mapper, ILoggerFactory loggerFactory)
        : base(boardRepository: boardRepository, mapper: mapper)
        {
            _logger = loggerFactory.CreateLogger<GetSingleSvgBoardByBoardIdAndColorsHandler>();
            _loggingMessages = new LoggingMessages(_logger);
        }

        public async Task<SvgDocument> Handle(GetSingleSvgBoardByBoardIdAndColorsQuery request, CancellationToken cancellationToken)
        {
            var boardEntity = await BoardRepository.GetItemAsync(request.BoardId);
            if (boardEntity == null)
            {
                throw new EntityNotFoundException(nameof(BoardEntity), request.BoardId);
            }
            var boardEntityRes = Mapper.Map(boardEntity, new BoardEntity());

            var boardSvgBuilder = new BoardSvgBuilder(boardEntityRes, request.Color1, request.Color2, request.Color3);
            if (boardSvgBuilder == null)
            {
                //TODO: I don't think this will ever get called because the colors are validated. Might be fine to remove later.
                _loggingMessages.LogEmptyReturnVariableWithParameters(
                    nameof(GetSingleSvgBoardByBoardIdAndColorsHandler),
                    nameof(BoardSvgBuilder),
                    $"Color1:{request.Color1},Color2:{request.Color2},Color3:{request.Color3}"
                    );
                return null;
            }

            var svgBoardDoc = boardSvgBuilder.SvgBoardDoc;
            if (svgBoardDoc.HasChildren() == false)
            {
                //TODO: This might also never get called. Maybe remove later.
                _loggingMessages.LogEmptyReturnVariable(
                    nameof(GetSingleSvgBoardByBoardIdAndColorsHandler),
                    nameof(svgBoardDoc));
                return null;
            }

            return await Task.FromResult(svgBoardDoc);
        }
    }
}

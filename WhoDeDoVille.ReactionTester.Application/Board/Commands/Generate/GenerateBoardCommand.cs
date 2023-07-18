namespace WhoDeDoVille.ReactionTester.Application.Board.Commands.Generate;

/// <summary>
///     Generates game boards and inserts them into the database.
/// </summary>
/// <param name="DifficultyLevel">Board Difficulty Level</param>
/// <param name="SequenceNumber">
///     Needs to be a unique number. Supposed to be in sequence by difficulty.
/// </param>
/// <param name="BoardCount">Minimum amount of boards to generate</param>
/// <returns>Boolean if boards were successfully created</returns>
public class GenerateBoardCommand : ICommand<bool>
{
    public int DifficultyLevel { get; set; }
    public string SequenceNumber { get; set; }
    public int BoardCount { get; set; }
}

public class GenerateBoardHandler : ApplicationBase, IRequestHandler<GenerateBoardCommand, bool>
{
    private readonly ISender _sender;
    private readonly ILoggerFactory _loggerFactory;

    public GenerateBoardHandler(IBoardRepository boardRepository, IMapper mapper, ISender sender, ILoggerFactory loggerFactory)
        : base(boardRepository: boardRepository, mapper: mapper)
    {
        _sender = sender;
        _loggerFactory = loggerFactory;
    }

    public async Task<bool> Handle(GenerateBoardCommand request, CancellationToken cancellationToken)
    {
        List<DatabaseAndContainerNamesEnum> dcList = new() {
            DatabaseAndContainerNamesEnum.CONTAINER_BOARDSEQUENCE,
            DatabaseAndContainerNamesEnum.CONTAINER_BOARD,
            DatabaseAndContainerNamesEnum.CONTAINER_BOARDLIST };
        var dcResult = new DatabaseAndContainerBuilder(_sender, _loggerFactory) { DatabaseAndContainerNamesEnums = dcList };
        var dcResponse = await dcResult.InitializeDatabaseAndContainer();

        var boardListIdResponseData = await _sender.Send(new GetSingleBoardListByIdQuery
        {
            BoardListId = $"{request.DifficultyLevel}:{request.SequenceNumber}"
        });

        if (boardListIdResponseData.Id != null)
        {
            // TODO: Setup exception for when the sequence number already exists.
            return false;
        }

        var boardListGeneratorEntity = new BoardAndBoardListBuilder(
            request.DifficultyLevel,
            request.SequenceNumber,
            request.BoardCount,
            _sender
            );

        await boardListGeneratorEntity.GenerateUniqueBoards();

        var returnBool = await _sender.Send(new AddBoardListGeneratorResultsCommand
        {
            BoardAndBoardListBuilder = boardListGeneratorEntity
        });

        return returnBool;
    }
}

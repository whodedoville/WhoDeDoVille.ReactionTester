using WhoDeDoVille.ReactionTester.Application.BoardSequence.Queries;

namespace WhoDeDoVille.ReactionTester.Application.Board.Commands.Generate;

public class GenerateBoardGetNextSequenceCommand : ICommand<bool>
{
    public int DifficultyLevel { get; set; }
    public int? BoardCount { get; set; } = null;
}

public class GenerateBoardGetNextSequenceHandler : ApplicationBase, IRequestHandler<GenerateBoardGetNextSequenceCommand, bool>
{
    private readonly ISender _sender;
    private readonly ILoggerFactory _loggerFactory;

    public GenerateBoardGetNextSequenceHandler(IBoardRepository boardRepository, IMapper mapper, ISender sender, ILoggerFactory loggerFactory)
        : base(boardRepository: boardRepository, mapper: mapper)
    {
        _sender = sender;
        _loggerFactory = loggerFactory;
    }

    public async Task<bool> Handle(GenerateBoardGetNextSequenceCommand request, CancellationToken cancellationToken)
    {
        List<DatabaseAndContainerNamesEnum> dcList = new() {
            DatabaseAndContainerNamesEnum.CONTAINER_BOARDSEQUENCE,
            DatabaseAndContainerNamesEnum.CONTAINER_BOARD,
            DatabaseAndContainerNamesEnum.CONTAINER_BOARDLIST };
        var dcResult = new DatabaseAndContainerBuilder(_sender, _loggerFactory) { DatabaseAndContainerNamesEnums = dcList };
        var dcResponse = await dcResult.InitializeDatabaseAndContainer();

        var sequenceNumber = 1;
        var boardCount = BoardConfig.DefaultBoardCount;
        var boardSequence = await _sender.Send(new GetSingleBoardSequenceByIdQuery
        {
            BoardSequenceId = $"Board:{request.DifficultyLevel}"
        });

        if (boardSequence.SequenceNumber > 0) sequenceNumber = boardSequence.SequenceNumber + 1;
        if (request.BoardCount != null) boardCount = (int)request.BoardCount;

        var boardListGeneratorEntity = new BoardAndBoardListBuilder(
            request.DifficultyLevel,
            sequenceNumber,
            boardCount,
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
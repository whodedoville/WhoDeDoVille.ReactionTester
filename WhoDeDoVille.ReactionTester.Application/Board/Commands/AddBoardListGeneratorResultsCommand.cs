using WhoDeDoVille.ReactionTester.Application.BoardSequence.Commands;

namespace WhoDeDoVille.ReactionTester.Application.Board.Commands;

public class AddBoardListGeneratorResultsCommand : ICommand<bool>
{
    public BoardAndBoardListBuilder BoardAndBoardListBuilder { get; set; }
}

public class AddBoardListGeneratorResultsHandler : ApplicationBase, IRequestHandler<AddBoardListGeneratorResultsCommand, bool>
{
    private readonly ISender _sender;

    public AddBoardListGeneratorResultsHandler(ISender sender)
        : base()
    {
        _sender = sender;
    }

    public async Task<bool> Handle(AddBoardListGeneratorResultsCommand request, CancellationToken cancellationToken)
    {
        //TODO: Set up validator for BoardAndBoardListBuilder. Setup entity validators then inherit from that. I think.
        //TODO: Error check. Need to make it so if there is an error along the way it will remove any previously entered data.
        var boardListGeneratorEntity = request.BoardAndBoardListBuilder;

        var boardSequenceEntity = boardListGeneratorEntity.GetValidatedBoardSequenceEntity();
        var boardSequenceResponseData = await _sender.Send(new UpdateOrAddBoardSequenceCommand
        {
            BoardSequenceId = boardSequenceEntity.Id,
            SequenceNumber = boardSequenceEntity.SequenceNumber,
            CreatedDt = boardSequenceEntity.CreatedDt
        });

        var boardListEntity = boardListGeneratorEntity.GetValidatedBoardListEntity();
        var boardListResponseData = await _sender.Send(new AddBoardListCommand
        {
            DifficultyLevel = boardListEntity.Difficulty,
            SequenceNumber = boardListEntity.SequenceNumber,
            CreatedDt = boardListEntity.CreatedDt,
            BoardIdList = boardListEntity.BoardIdList
        });

        var boardEntityList = boardListGeneratorEntity.GetValidatedBoardEntityList();
        var boardEntityListResponseData = await _sender.Send(new AddManyBoardCommand
        {
            BoardEntityList = boardEntityList
        });

        return true;
    }
}
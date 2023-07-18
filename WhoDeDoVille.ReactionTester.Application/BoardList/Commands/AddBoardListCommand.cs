namespace WhoDeDoVille.ReactionTester.Application.BoardList.Commands;

/// <summary>
/// Adds Board List item to database.
/// This item is used along with Board items for tracking boards.
/// </summary>
/// <param name="Difficulty">Board difficulty level</param>
/// <param name="SequenceNumber">
/// Needs to be a unique number. Supposed to be in sequence by difficulty.
/// </param>
/// <param name="CreatedDt">When boards were created.</param>
/// <param name="BoardIdList">List of board hashes created.</param>
/// <returns>Board List responses</returns>
public class AddBoardListCommand : ICommand<BoardListEntity>
{
    public int DifficultyLevel { get; set; }
    public string SequenceNumber { get; set; }
    public DateTime CreatedDt { get; set; }
    public List<string>? BoardIdList { get; set; } = new List<string>();

    public class AddNewBoardListHandler : ApplicationBase, IRequestHandler<AddBoardListCommand, BoardListEntity>
    {
        public AddNewBoardListHandler(IBoardListRepository boardListRepository, IMapper mapper)
            : base(boardListRepository: boardListRepository, mapper: mapper) { }

        public async Task<BoardListEntity> Handle(AddBoardListCommand request, CancellationToken cancellationToken)
        {
            BoardListEntity boardListEntity = new()
            {
                Difficulty = request.DifficultyLevel,
                SequenceNumber = request.SequenceNumber,
                CreatedDt = request.CreatedDt,
                BoardIdList = request.BoardIdList
            };

            await BoardListRepository.AddItemAsync(boardListEntity);

            var res = Mapper.Map(boardListEntity, new BoardListEntity());
            return await Task.FromResult(res);
        }
    }
}

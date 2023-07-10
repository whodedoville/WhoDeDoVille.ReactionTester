namespace WhoDeDoVille.ReactionTester.Application.Board.Commands;

/// <summary>
/// Sends many boards to the database in bulk.
/// Needs AllowBulkExecution to be true. <see cref="RegisterService.AddInfrastructure"/>
/// Works with BoardList to add board entries into the database.
/// </summary>
/// <param name="BoardEntityList">
/// List of boards to be added to the database
/// </param>
/// <returns>List of BoardEntity</returns>
public class AddManyBoardCommand : ICommand<List<BoardEntity>>
{
    public List<BoardEntity>? BoardEntityList { get; set; } = new List<BoardEntity>();

    public class AddManyBoardHandler : ApplicationBase, IRequestHandler<AddManyBoardCommand, List<BoardEntity>>
    {
        public AddManyBoardHandler(IBoardRepository boardRepository, IMapper mapper)
            : base(boardRepository: boardRepository, mapper: mapper) { }

        public async Task<List<BoardEntity>> Handle(AddManyBoardCommand request, CancellationToken cancellationToken)
        {
            //TODO: Set up validator for BoardEntityList. Setup entity validators then inherit from that. I think.
            List<BoardEntity> boardEntities = request.BoardEntityList;

            //await BoardRepository.AddItemAsync(boardEntities[0]);

            await BoardRepository.AddItemsAsync(boardEntities);

            var res = Mapper.Map(boardEntities, new List<BoardEntity>());
            return await Task.FromResult(res);
        }
    }
}

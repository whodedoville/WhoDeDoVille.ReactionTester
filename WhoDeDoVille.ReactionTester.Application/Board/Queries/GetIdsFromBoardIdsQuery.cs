namespace WhoDeDoVille.ReactionTester.Application.Board.Queries;

/// <summary>
///     Gets Ids from Board container
/// </summary>
/// <param name="BoardId"></param>
/// <returns>List of BoardEntity Ids</returns>
public class GetIdsFromBoardIdsQuery : IQuery<List<BoardIdDTO>>
{
    public List<string> BoardIdList { get; set; } = new List<string>();

    public class GetIdsFromBoardIdsHandler : ApplicationBase, IRequestHandler<GetIdsFromBoardIdsQuery, List<BoardIdDTO>>
    {
        public GetIdsFromBoardIdsHandler(IBoardRepository boardRepository, IMapper mapper)
        : base(boardRepository: boardRepository, mapper: mapper) { }

        public async Task<List<BoardIdDTO>> Handle(GetIdsFromBoardIdsQuery request, CancellationToken cancellationToken)
        {
            var data = await BoardRepository.GetIdsByIds(request.BoardIdList);
            var res = Mapper.Map(data, new List<BoardIdDTO>());
            return await Task.FromResult(res);
        }
    }
}

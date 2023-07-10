namespace WhoDeDoVille.ReactionTester.Application.Board.Queries;

/// <summary>
///     Gets Board from id for client;
/// </summary>
/// <param name="BoardId">Board Id</param>
/// <returns>BoardClientDTO</returns>
public class GetSingleBoardByIdForClientQuery : IQuery<BoardClientDTO>
{
    public string BoardId { get; set; }

    public class GetSingleBoardByIdForClientHandler : ApplicationBase, IRequestHandler<GetSingleBoardByIdForClientQuery, BoardClientDTO>
    {
        public GetSingleBoardByIdForClientHandler(IBoardRepository boardRepository, IMapper mapper)
        : base(boardRepository: boardRepository, mapper: mapper) { }

        public async Task<BoardClientDTO> Handle(GetSingleBoardByIdForClientQuery request, CancellationToken cancellationToken)
        {
            var data = await BoardRepository.GetItemAsync(request.BoardId);
            var res = Mapper.Map(data, new BoardClientDTO());
            return await Task.FromResult(res);
        }
    }
}

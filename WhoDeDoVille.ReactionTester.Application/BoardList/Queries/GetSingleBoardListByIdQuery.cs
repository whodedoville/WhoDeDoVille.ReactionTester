namespace WhoDeDoVille.ReactionTester.Application.BoardList.Queries;

/// <summary>
///     Gets Board List by Id from database.
/// </summary>
/// <param name="BoardListId">Board List Id</param>
/// <returns>BoardListEntity</returns>
public class GetSingleBoardListByIdQuery : IRequest<BoardListIdDTO>
{
    public string BoardListId { get; set; }
    public class GetSingleBoardListByIdHandler : ApplicationBase, IRequestHandler<GetSingleBoardListByIdQuery, BoardListIdDTO>
    {
        public GetSingleBoardListByIdHandler(IBoardListRepository boardListRepository, IMapper mapper)
            : base(boardListRepository: boardListRepository, mapper: mapper) { }

        public async Task<BoardListIdDTO> Handle(GetSingleBoardListByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await BoardListRepository.GetItemAsync(request.BoardListId);
            var res = Mapper.Map(data, new BoardListIdDTO());
            return await Task.FromResult(res);
        }
    }
}

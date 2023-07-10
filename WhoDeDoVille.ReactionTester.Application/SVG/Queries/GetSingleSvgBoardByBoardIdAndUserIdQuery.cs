using Svg;

namespace WhoDeDoVille.ReactionTester.Application.SVG.Queries;

/// <summary>
///     Gets Svg Board By Board Id
/// </summary>
/// <param name="Id"></param>
/// <returns>Svg Document</returns>
public class GetSingleSvgBoardByBoardIdAndUserIdQuery : IQuery<SvgDocument>
{
    public string BoardId { get; set; }
    public string UserId { get; set; }

    public class GetSingleSvgBoardByBoardIdHandler : ApplicationBase, IRequestHandler<GetSingleSvgBoardByBoardIdAndUserIdQuery, SvgDocument>
    {
        public GetSingleSvgBoardByBoardIdHandler(
            IUserRepository userRepository,
            IBoardRepository boardRepository,
            IMapper mapper)
        : base(userRepository: userRepository, boardRepository: boardRepository, mapper: mapper) { }

        public async Task<SvgDocument> Handle(GetSingleSvgBoardByBoardIdAndUserIdQuery request, CancellationToken cancellationToken)
        {
            var userData = await UserRepository.GetItemAsync(request.UserId);
            var boardData = await BoardRepository.GetItemAsync(request.BoardId);

            var res = Mapper.Map(boardData, new SvgDocument());
            return await Task.FromResult(res);
        }
    }
}

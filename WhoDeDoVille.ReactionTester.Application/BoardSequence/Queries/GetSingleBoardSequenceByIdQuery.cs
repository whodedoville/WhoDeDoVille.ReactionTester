namespace WhoDeDoVille.ReactionTester.Application.BoardSequence.Queries;

/// <summary>
/// Get Board Sequence from id
/// </summary>
/// /// <param name="Id">Board Id</param>
/// <returns>BoardSequenceDTO</returns>
public class GetSingleBoardSequenceByIdQuery : IQuery<BoardSequenceDTO>
{
    public string BoardSequenceId { get; set; }

    public class GetSingleBoardSequenceByIdHandler : ApplicationBase, IRequestHandler<GetSingleBoardSequenceByIdQuery, BoardSequenceDTO>
    {
        public GetSingleBoardSequenceByIdHandler(IBoardSequenceRepository boardSequenceRepository, IMapper mapper)
        : base(boardSequenceRepository: boardSequenceRepository, mapper: mapper) { }

        public async Task<BoardSequenceDTO> Handle(GetSingleBoardSequenceByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await BoardSequenceRepository.GetItemAsync(request.BoardSequenceId);
            var res = Mapper.Map(data, new BoardSequenceDTO());
            return await Task.FromResult(res);
        }
    }
}

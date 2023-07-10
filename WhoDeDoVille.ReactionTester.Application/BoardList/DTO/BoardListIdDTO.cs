namespace WhoDeDoVille.ReactionTester.Application.BoardList.DTO;

/// <summary>
/// Board Id
/// </summary>
/// <param name="Id">Board Id</param>
public class BoardListIdDTO : IMapFrom<BoardListEntity>
{
    public string? Id { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<BoardListEntity, BoardListIdDTO>();
        profile.CreateMap<BoardListIdDTO, BoardListEntity>();
    }
}

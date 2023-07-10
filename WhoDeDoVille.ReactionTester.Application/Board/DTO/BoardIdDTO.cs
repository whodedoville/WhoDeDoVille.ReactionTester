

namespace WhoDeDoVille.ReactionTester.Application.Board.DTO;

/// <summary>
/// To check if there is an entry in the Board container.
/// </summary>
/// <returns>board ids</returns>
public class BoardIdDTO : IMapFrom<BoardEntity>
{
    public string? Id { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BoardEntity, BoardIdDTO>();
        profile.CreateMap<BoardIdDTO, BoardEntity>();
    }
}

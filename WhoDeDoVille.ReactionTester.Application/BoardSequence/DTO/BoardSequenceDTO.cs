namespace WhoDeDoVille.ReactionTester.Application.BoardSequence.DTO;

public class BoardSequenceDTO : IMapFrom<BoardSequenceEntity>
{
    public int SequenceNumber { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BoardSequenceEntity, BoardSequenceDTO>();
        profile.CreateMap<BoardSequenceDTO, BoardSequenceEntity>();
    }
}

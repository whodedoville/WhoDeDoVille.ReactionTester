namespace WhoDeDoVille.ReactionTester.Application.Board.DTO
{
    public class BoardClientDTO : IMapFrom<BoardEntity>
    {
        public List<BoardAnswerEntity> Answer { get; set; }
        public BoardDirectionsCoordsEntity DirectionsCoords { get; set; }
        public List<BoardShapeEntity> Shapes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BoardEntity, BoardClientDTO>();
            profile.CreateMap<BoardClientDTO, BoardEntity>();
        }
    }
}

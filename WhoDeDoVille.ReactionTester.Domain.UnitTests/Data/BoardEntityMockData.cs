namespace WhoDeDoVille.ReactionTester.Domain.UnitTests.Data;

public static class BoardEntityMockData
{
    public static readonly List<BoardEntity> boardEntityMockDataList = new()
    {
        new BoardEntity()
        {
            Id = "3:3:7ad2337a56d22741285b57ac3813f10c0b3432d3c084a4e16caf4537b6e45387",
            DifficultySequence = "1:1",
            PartitionKey = "3:3:7ad233",
            CreatedDt = DateTime.Parse("0001-01-01T00:00:00"),
            Answer = new List<BoardAnswerEntity>()
            {
                new BoardAnswerEntity() { X = 16, Y = 2 },
                new BoardAnswerEntity() { X = 9, Y = 9 },
                new BoardAnswerEntity() { X = 17, Y = 17 },
            },
            DirectionsCoords = new BoardDirectionsCoordsEntity()
            {
                StartX = 4,
                StartY = 1,
                EndX = 8,
                EndY = 1,
            },
            Shapes = new List<BoardShapeEntity>()
            {
                new BoardShapeEntity() { X = 16, Y = 2, Shape = ShapesEnum.CIRCLE, Color = ColorsEnum.COLOR1 },
                new BoardShapeEntity() { X = 9, Y = 9, Shape = ShapesEnum.SQUARE, Color = ColorsEnum.COLOR2 },
                new BoardShapeEntity() { X = 17, Y = 17, Shape = ShapesEnum.SQUARE, Color = ColorsEnum.COLOR1 },
            }
        }
    };
}

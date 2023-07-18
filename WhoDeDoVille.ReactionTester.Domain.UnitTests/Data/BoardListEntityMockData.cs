namespace WhoDeDoVille.ReactionTester.Domain.UnitTests.Data;

public static class BoardListEntityMockData
{
    public static readonly List<BoardListEntity> boardListEntityMockDataList = new()
    {
        new BoardListEntity
        {
            Id = "1:1",
            Difficulty = 1,
            SequenceNumber = "1",
            CreatedDt = DateTime.Parse("0001-01-01T00:00:00"),
            BoardIdList = new List<string>()
            {
                "3:3:7ad2337a56d22741285b57ac3813f10c0b3432d3c084a4e16caf4537b6e45387"
            }
        }
    };
}

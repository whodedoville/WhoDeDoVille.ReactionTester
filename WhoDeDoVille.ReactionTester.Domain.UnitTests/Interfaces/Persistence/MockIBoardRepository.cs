



using WhoDeDoVille.ReactionTester.Domain.UnitTests.Data;

namespace WhoDeDoVille.ReactionTester.Domain.UnitTests.Interfaces.Persistence;

public class MockIBoardRepository
{
    public static Mock<IBoardRepository> GetMock()
    {
        // TODO: Finish mocking repositories
        var mock = new Mock<IBoardRepository>();

        var boardEntities = BoardEntityMockData.boardEntityMockDataList;

        //mock.Setup(m => m.GetIdsByIds(It.IsAny<List<string>>()))
        //    .Returns((List<string> id) => boardEntities.Where(a => id.Contains(a.Id) == true).ToList());

        mock.Setup(m => m.GetItemAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) => boardEntities.First(o => o.Id == id));

        return mock;
    }
}

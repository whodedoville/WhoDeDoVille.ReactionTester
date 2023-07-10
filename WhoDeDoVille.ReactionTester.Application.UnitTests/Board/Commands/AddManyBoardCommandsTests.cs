

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.Board.Commands;

public class AddManyBoardCommandsTests
{
    private readonly Mock<IBoardRepository> _boardRepositoryMock;

    public AddManyBoardCommandsTests()
    {
        _boardRepositoryMock = new();
    }
}

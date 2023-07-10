using WhoDeDoVille.ReactionTester.Application.BoardList.Queries;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.BoardList.Queries;

public class GetSingleBoardListByIdQueryValidatorTests
{
    private readonly GetSingleBoardListByIdQueryValidator _getSingleBoardListByIdQueryValidator;

    public GetSingleBoardListByIdQueryValidatorTests()
    {
        _getSingleBoardListByIdQueryValidator = new GetSingleBoardListByIdQueryValidator();
    }

    [Theory]
    [InlineData("1:1")]
    [InlineData("9:200000")]
    [InlineData("5:200")]
    public void Given_GetSingleBoardListByIdQuery_Is_Valid(string BoardListId)
    {
        // Arrange
        var generateBoardListContainerCommand = new GetSingleBoardListByIdQuery
        {
            BoardListId = BoardListId
        };

        // Act
        var response = _getSingleBoardListByIdQueryValidator.TestValidate(generateBoardListContainerCommand);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.BoardListId);
    }
}

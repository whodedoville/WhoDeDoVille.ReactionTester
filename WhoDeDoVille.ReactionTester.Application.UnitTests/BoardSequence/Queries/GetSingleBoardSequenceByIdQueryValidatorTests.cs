using WhoDeDoVille.ReactionTester.Application.BoardSequence.Queries;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.BoardSequence.Queries;

public class GetSingleBoardSequenceByIdQueryValidatorTests
{
    private readonly GetSingleBoardSequenceByIdQueryValidator _getSingleBoardSequenceByIdQueryValidator;

    public GetSingleBoardSequenceByIdQueryValidatorTests()
    {
        _getSingleBoardSequenceByIdQueryValidator = new GetSingleBoardSequenceByIdQueryValidator();
    }

    [Theory]
    [InlineData("Board:1")]
    [InlineData("Board:9")]
    public void Given_GetSingleBoardSequenceByIdQueryValidatorTests_Is_Valid(string BoardSequenceId)
    {
        // Arrange
        var getSingleBoardSequenceByIdQuery = new GetSingleBoardSequenceByIdQuery
        {
            BoardSequenceId = BoardSequenceId,
        };

        // Act
        var response = _getSingleBoardSequenceByIdQueryValidator.TestValidate(getSingleBoardSequenceByIdQuery);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.BoardSequenceId);
    }

    [Theory]
    [InlineData("Board:0")]
    [InlineData("Board:10")]
    public void Given_GetSingleBoardSequenceByIdQueryValidatorTests_Is_Invalid(string BoardSequenceId)
    {
        // Arrange
        var getSingleBoardSequenceByIdQuery = new GetSingleBoardSequenceByIdQuery
        {
            BoardSequenceId = BoardSequenceId,
        };

        // Act
        var response = _getSingleBoardSequenceByIdQueryValidator.TestValidate(getSingleBoardSequenceByIdQuery);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.BoardSequenceId);
    }
}

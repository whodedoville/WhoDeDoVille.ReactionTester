using WhoDeDoVille.ReactionTester.Application.Board.Commands.Generate;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.Board.Commands.Generate;

public class GenerateBoardCommandValidatorTests
{
    private readonly GenerateBoardCommandValidator _generateBoardCommandValidator;

    public GenerateBoardCommandValidatorTests()
    {
        _generateBoardCommandValidator = new GenerateBoardCommandValidator();
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(7, 1000, 100)]
    public void Given_GenerateBoardCommandValidator_Is_Valid(
        int DifficultyLevel,
        int SequenceNum,
        int BoardCount)
    {
        // Arrange
        GenerateBoardCommand generateBoardCommand = new GenerateBoardCommand
        {
            DifficultyLevel = DifficultyLevel,
            SequenceNumber = SequenceNum,
            BoardCount = BoardCount
        };

        // Act
        var response = _generateBoardCommandValidator.TestValidate(generateBoardCommand);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.DifficultyLevel);
        response.ShouldNotHaveValidationErrorFor(x => x.SequenceNumber);
        response.ShouldNotHaveValidationErrorFor(x => x.BoardCount);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, -1, 101)]
    public void Given_GenerateBoardCommandValidator_Is_Invalid(
        int DifficultyLevel,
        int SequenceNum,
        int BoardCount)
    {
        // Arrange
        GenerateBoardCommand generateBoardCommand = new GenerateBoardCommand
        {
            DifficultyLevel = DifficultyLevel,
            SequenceNumber = SequenceNum,
            BoardCount = BoardCount
        };

        // Act
        var response = _generateBoardCommandValidator.TestValidate(generateBoardCommand);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.DifficultyLevel);
        response.ShouldHaveValidationErrorFor(x => x.SequenceNumber);
        response.ShouldHaveValidationErrorFor(x => x.BoardCount);
    }
}

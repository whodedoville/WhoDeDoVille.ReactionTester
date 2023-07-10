using WhoDeDoVille.ReactionTester.Application.Board.Commands.Generate;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.Board.Commands.Generate;

public class GenerateBoardGetNextSequenceCommandValidatorTests
{
    private readonly GenerateBoardGetNextSequenceCommandValidator _generateBoardGetNextSequenceCommandValidator;

    public GenerateBoardGetNextSequenceCommandValidatorTests()
    {
        _generateBoardGetNextSequenceCommandValidator = new GenerateBoardGetNextSequenceCommandValidator();
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(7, 100)]
    [InlineData(3, null)]
    public void Given_GenerateBoardGetNextSequenceCommandValidator_Is_Valid(int DifficultyLevel, int? BoardCount)
    {
        // Arrange
        var generateBoardGetNextSequenceCommand = new GenerateBoardGetNextSequenceCommand
        {
            DifficultyLevel = DifficultyLevel,
            BoardCount = BoardCount
        };

        // Act
        var response = _generateBoardGetNextSequenceCommandValidator.TestValidate(generateBoardGetNextSequenceCommand);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.DifficultyLevel);
        response.ShouldNotHaveValidationErrorFor(x => x.BoardCount);
    }

    [Theory]
    [InlineData(0, 101)]
    [InlineData(0, -1)]
    [InlineData(0, 0)]
    public void Given_GenerateBoardGetNextSequenceCommandValidator_Is_Invalid(int DifficultyLevel, int? BoardCount)
    {
        // Arrange
        var generateBoardGetNextSequenceCommand = new GenerateBoardGetNextSequenceCommand
        {
            DifficultyLevel = DifficultyLevel,
            BoardCount = BoardCount
        };

        // Act
        var response = _generateBoardGetNextSequenceCommandValidator.TestValidate(generateBoardGetNextSequenceCommand);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.DifficultyLevel);
        response.ShouldHaveValidationErrorFor(x => x.BoardCount);
    }
}

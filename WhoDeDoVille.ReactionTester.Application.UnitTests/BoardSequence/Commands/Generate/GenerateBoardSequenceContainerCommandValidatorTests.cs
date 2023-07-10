using WhoDeDoVille.ReactionTester.Application.BoardSequence.Commands.Generate;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.BoardSequence.Commands.Generate;

public class GenerateBoardSequenceContainerCommandValidatorTests
{
    private readonly GenerateBoardSequenceContainerCommandValidator _generateBoardSequenceContainerCommandValidator;

    public GenerateBoardSequenceContainerCommandValidatorTests()
    {
        _generateBoardSequenceContainerCommandValidator = new GenerateBoardSequenceContainerCommandValidator();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Given_GenerateBoardListContainerCommandValidator_Is_Valid(bool CheckInitialized)
    {
        // Arrange
        var generateBoardSequenceContainerCommand = new GenerateBoardSequenceContainerCommand
        {
            CheckInitialized = CheckInitialized
        };

        // Act
        var response = _generateBoardSequenceContainerCommandValidator.TestValidate(generateBoardSequenceContainerCommand);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.CheckInitialized);
    }
}

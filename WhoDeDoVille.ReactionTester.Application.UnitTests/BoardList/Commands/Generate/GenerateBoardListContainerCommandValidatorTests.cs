using WhoDeDoVille.ReactionTester.Application.BoardList.Commands.Generate;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.BoardList.Commands.Generate;

public class GenerateBoardListContainerCommandValidatorTests
{
    private readonly GenerateBoardListContainerCommandValidator _generateBoardListContainerCommandValidator;

    public GenerateBoardListContainerCommandValidatorTests()
    {
        _generateBoardListContainerCommandValidator = new GenerateBoardListContainerCommandValidator();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Given_GenerateBoardListContainerCommandValidator_Is_Valid(bool CheckInitialized)
    {
        // Arrange
        var generateBoardListContainerCommand = new GenerateBoardListContainerCommand
        {
            CheckInitialized = CheckInitialized
        };

        // Act
        var response = _generateBoardListContainerCommandValidator.TestValidate(generateBoardListContainerCommand);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.CheckInitialized);
    }
}

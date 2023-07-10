using WhoDeDoVille.ReactionTester.Application.ReactionTester.Commands.Generate;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.ReactionTester.Commands.Generate;

public class GenerateReactionTesterDatabaseCommandValidatorTests
{
    private readonly GenerateReactionTesterDatabaseCommandValidator _generateReactionTesterDatabaseCommandValidator;

    public GenerateReactionTesterDatabaseCommandValidatorTests()
    {
        _generateReactionTesterDatabaseCommandValidator = new GenerateReactionTesterDatabaseCommandValidator();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Given_GenerateBoardListContainerCommandValidator_Is_Valid(bool CheckInitialized)
    {
        // Arrange
        var generateReactionTesterDatabaseCommand = new GenerateReactionTesterDatabaseCommand
        {
            CheckInitialized = CheckInitialized
        };

        // Act
        var response = _generateReactionTesterDatabaseCommandValidator.TestValidate(generateReactionTesterDatabaseCommand);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.CheckInitialized);
    }
}

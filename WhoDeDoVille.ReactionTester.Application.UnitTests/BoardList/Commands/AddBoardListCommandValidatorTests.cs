using WhoDeDoVille.ReactionTester.Application.BoardList.Commands;
using WhoDeDoVille.ReactionTester.Infrastructure.Common;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.BoardList.Commands;

public class AddBoardListCommandValidatorTests
{
    private readonly AddBoardListCommandValidator _addBoardListCommandValidator;

    public AddBoardListCommandValidatorTests()
    {
        _addBoardListCommandValidator = new AddBoardListCommandValidator();
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(2, "2")]
    public void Given_GenerateBoardGetNextSequenceCommandValidator_Is_Valid(
        int DifficultyLevel,
        string SequenceNumber)
    {
        // Arrange
        var addBoardListCommand = new AddBoardListCommand
        {
            DifficultyLevel = DifficultyLevel,
            SequenceNumber = SequenceNumber,
            CreatedDt = DateTimeProvider.UtcNow,
            BoardIdList = new List<string>() { "3:3:8fc1726efe704bab9c246dc24f96b784f5925fd14ec33985eef340833d6b0d39" }
        };

        // Act
        var response = _addBoardListCommandValidator.TestValidate(addBoardListCommand);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.DifficultyLevel);
        response.ShouldNotHaveValidationErrorFor(x => x.SequenceNumber);
        response.ShouldNotHaveValidationErrorFor(x => x.CreatedDt);
        response.ShouldNotHaveValidationErrorFor(x => x.BoardIdList);
    }
}

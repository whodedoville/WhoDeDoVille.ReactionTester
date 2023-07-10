using WhoDeDoVille.ReactionTester.Application.BoardSequence.Commands;

namespace WhoDeDoVille.ReactionTester.Application.UnitTests.BoardSequence.Commands;

public class UpdateOrAddBoardSequenceCommandValidatorTests
{
    private readonly UpdateOrAddBoardSequenceCommandValidator _updateOrAddBoardSequenceCommandValidator;

    public UpdateOrAddBoardSequenceCommandValidatorTests()
    {
        _updateOrAddBoardSequenceCommandValidator = new UpdateOrAddBoardSequenceCommandValidator();
    }

    [Theory]
    [InlineData("Board:1", 1)]
    [InlineData("Board:9", 2)]
    public void Given_UpdateOrAddBoardSequenceCommandValidatorTests_Is_Valid(
        string BoardSequenceId,
        int SequenceNumber)
    {
        // Arrange
        var updateOrAddBoardSequenceCommand = new UpdateOrAddBoardSequenceCommand
        {
            BoardSequenceId = BoardSequenceId,
            SequenceNumber = SequenceNumber,
            CreatedDt = DateTime.Now
        };

        // Act
        var response = _updateOrAddBoardSequenceCommandValidator.TestValidate(updateOrAddBoardSequenceCommand);

        // Assert
        response.ShouldNotHaveValidationErrorFor(x => x.BoardSequenceId);
        response.ShouldNotHaveValidationErrorFor(x => x.SequenceNumber);
        response.ShouldNotHaveValidationErrorFor(x => x.CreatedDt);
    }

    [Theory]
    [InlineData("Board:0", null)]
    [InlineData("Board:10", 0)]
    public void Given_UpdateOrAddBoardSequenceCommandValidatorTests_Is_Invalid(
        string BoardSequenceId,
        int SequenceNumber)
    {
        // Arrange
        var updateOrAddBoardSequenceCommand = new UpdateOrAddBoardSequenceCommand
        {
            BoardSequenceId = BoardSequenceId,
            SequenceNumber = SequenceNumber,
            CreatedDt = DateTime.Now
        };

        // Act
        var response = _updateOrAddBoardSequenceCommandValidator.TestValidate(updateOrAddBoardSequenceCommand);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.BoardSequenceId);
        response.ShouldHaveValidationErrorFor(x => x.SequenceNumber);
        response.ShouldNotHaveValidationErrorFor(x => x.CreatedDt);
    }
}

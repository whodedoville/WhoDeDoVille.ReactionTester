namespace WhoDeDoVille.ReactionTester.Application.BoardList.Commands;

public class AddBoardListCommandValidator : AbstractValidator<AddBoardListCommand>
{
    public AddBoardListCommandValidator()
    {
        RuleFor(v => v.DifficultyLevel).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(BoardConfig.DifficultyLevelSettings.Count);
        //RuleFor(v => v.SequenceNumber).NotEmpty().GreaterThanOrEqualTo(1);
        RuleFor(v => v.SequenceNumber).NotEmpty().Must(val => Convert.ToInt32(val) >= 1);
        RuleFor(v => v.CreatedDt).NotNull();
        RuleForEach(v => v.BoardIdList).Matches(ValidationValuesProvider.BoardIdRegex);
    }
}

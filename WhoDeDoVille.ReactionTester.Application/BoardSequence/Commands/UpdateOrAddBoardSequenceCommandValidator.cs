namespace WhoDeDoVille.ReactionTester.Application.BoardSequence.Commands;

public class UpdateOrAddBoardSequenceCommandValidator : AbstractValidator<UpdateOrAddBoardSequenceCommand>
{
    public UpdateOrAddBoardSequenceCommandValidator()
    {
        RuleFor(v => v.BoardSequenceId).Matches(ValidationValuesProvider.BoardSequenceIdRegex);
        //RuleFor(v => v.SequenceNumber).NotEmpty().GreaterThanOrEqualTo(1);
        RuleFor(v => v.SequenceNumber).NotEmpty().Must(val => Convert.ToInt32(val) >= 1);
        RuleFor(v => v.CreatedDt).NotNull();
    }
}

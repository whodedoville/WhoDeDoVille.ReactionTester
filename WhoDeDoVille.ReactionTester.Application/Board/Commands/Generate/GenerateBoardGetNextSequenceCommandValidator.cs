namespace WhoDeDoVille.ReactionTester.Application.Board.Commands.Generate;

public class GenerateBoardGetNextSequenceCommandValidator : AbstractValidator<GenerateBoardGetNextSequenceCommand>
{
    public GenerateBoardGetNextSequenceCommandValidator()
    {
        RuleFor(v => v.DifficultyLevel).GreaterThanOrEqualTo(1).LessThanOrEqualTo(BoardConfig.DifficultyLevelSettings.Count);
        RuleFor(v => v.BoardCount)
            .GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).When(v => v.BoardCount is not null, ApplyConditionTo.AllValidators)
            .Null().When(v => v.BoardCount is null, ApplyConditionTo.CurrentValidator);
    }
}

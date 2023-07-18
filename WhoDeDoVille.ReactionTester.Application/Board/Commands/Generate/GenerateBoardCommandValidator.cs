namespace WhoDeDoVille.ReactionTester.Application.Board.Commands.Generate;

public class GenerateBoardCommandValidator : AbstractValidator<GenerateBoardCommand>
{
    public GenerateBoardCommandValidator()
    {
        RuleFor(v => v.DifficultyLevel).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(BoardConfig.DifficultyLevelSettings.Count);
        //RuleFor(v => v.SequenceNumber).NotEmpty().GreaterThanOrEqualTo(1);
        RuleFor(v => v.SequenceNumber).NotEmpty().Must(val => Convert.ToInt32(val) >= 1);
        RuleFor(v => v.BoardCount).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
    }
}

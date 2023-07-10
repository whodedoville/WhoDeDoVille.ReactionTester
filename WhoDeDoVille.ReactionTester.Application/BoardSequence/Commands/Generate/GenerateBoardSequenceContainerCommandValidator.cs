namespace WhoDeDoVille.ReactionTester.Application.BoardSequence.Commands.Generate;

public class GenerateBoardSequenceContainerCommandValidator : AbstractValidator<GenerateBoardSequenceContainerCommand>
{
    public GenerateBoardSequenceContainerCommandValidator()
    {
        RuleFor(v => v.CheckInitialized).NotNull();
    }
}

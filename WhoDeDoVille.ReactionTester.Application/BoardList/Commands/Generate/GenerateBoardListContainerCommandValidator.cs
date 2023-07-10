namespace WhoDeDoVille.ReactionTester.Application.BoardList.Commands.Generate;

public class GenerateBoardListContainerCommandValidator : AbstractValidator<GenerateBoardListContainerCommand>
{
    public GenerateBoardListContainerCommandValidator()
    {
        RuleFor(v => v.CheckInitialized).NotNull();
    }
}

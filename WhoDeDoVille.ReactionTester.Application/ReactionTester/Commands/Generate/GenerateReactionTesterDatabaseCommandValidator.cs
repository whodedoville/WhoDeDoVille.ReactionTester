namespace WhoDeDoVille.ReactionTester.Application.ReactionTester.Commands.Generate;

public class GenerateReactionTesterDatabaseCommandValidator : AbstractValidator<GenerateReactionTesterDatabaseCommand>
{
    public GenerateReactionTesterDatabaseCommandValidator()
    {
        RuleFor(v => v.CheckInitialized).NotNull();
    }
}

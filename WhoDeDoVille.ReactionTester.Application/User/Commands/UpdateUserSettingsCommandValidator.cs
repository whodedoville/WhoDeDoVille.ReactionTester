namespace WhoDeDoVille.ReactionTester.Application.User.Commands;

/// <summary>
/// User settings validation.
/// </summary>
public class UpdateUserSettingsCommandValidator : AbstractValidator<UpdateUserSettingsCommand>
{
    private string ColorRegEx = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
    private string ColorMessage = "Invalid color";
    public UpdateUserSettingsCommandValidator()
    {
        this.RuleFor(v => v.Color1).Matches(ColorRegEx).WithMessage(ColorMessage);
        this.RuleFor(v => v.Color2).Matches(ColorRegEx).WithMessage(ColorMessage);
        this.RuleFor(v => v.Color3).Matches(ColorRegEx).WithMessage(ColorMessage);
        this.RuleFor(v => v.DifficultyLevel).GreaterThan(0).LessThanOrEqualTo(10);
        this.RuleFor(v => v.IsAi).NotNull();
        this.RuleFor(v => v.Music).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
        this.RuleFor(v => v.Sound).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
    }
}

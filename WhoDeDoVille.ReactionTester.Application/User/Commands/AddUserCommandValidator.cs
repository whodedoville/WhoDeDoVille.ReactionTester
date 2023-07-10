namespace WhoDeDoVille.ReactionTester.Application.User.Commands;

/// <summary>
/// User validation.
/// </summary>
public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(v => v.Email).EmailAddress().WithMessage("Invalid email");
        RuleFor(v => v.Username).NotEmpty().MinimumLength(5).MaximumLength(15).WithMessage("Invalid Username");
    }
}

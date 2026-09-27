
namespace PMO.Application.Features.Users.Commands.Login;

public class loginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public loginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
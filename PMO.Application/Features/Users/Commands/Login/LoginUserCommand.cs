
namespace PMO.Application.Features.Users.Commands.Login;

public sealed record LoginUserCommand : IRequest<Result<LoginUserResponse>>
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}

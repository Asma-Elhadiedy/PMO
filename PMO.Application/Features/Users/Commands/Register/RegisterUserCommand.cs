
namespace PMO.Application.Features.Users.Commands.Register;

public sealed record RegisterUserCommand : IRequest<Result<bool>>
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}

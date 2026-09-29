
namespace PMO.Application.Features.Users.Commands.Logout;

public class LogoutUserCommandHandler(IIdentityService _identityService) : IRequestHandler<LogoutUserCommand>
{
    public Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        return _identityService.LogoutAsync("");
    }
}

namespace PMO.Application.Features.Users.Commands.Logout;

public class LogoutUserCommandHandler(IIdentityService _identityService) : IRequestHandler<LogoutUserCommand>
{
    public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await _identityService.LogoutAsync(request.UserId);
        return;
    }
}


namespace PMO.Application.Features.Users.Commands.Login;

public sealed class LoginUserCommandHandler(IIdentityService _identityService) : IRequestHandler<LoginUserCommand, Result<LoginUserResponse>>
{
    public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var tokenData = await _identityService.AuthenticateAsync(request.Email, request.Password);
        if (tokenData == null)
            return Result<LoginUserResponse>.Failure("Invalid email or password.");

        return Result<LoginUserResponse>.Success(tokenData);
    }
}

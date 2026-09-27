

namespace PMO.Application.Features.Users.Commands.Login;

public sealed class LoginUserCommandHandler(IIdentityService _identityService) : IRequestHandler<LoginUserCommand, Result<LoginUserResponse>>
{
    public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var token = await _identityService.AuthenticateAsync(request.Email, request.Password);
        if (string.IsNullOrEmpty(token))
            return Result<LoginUserResponse>.Failure("Invalid email or password.");

        return Result<LoginUserResponse>.Success(new() { Token = token });
    }
}

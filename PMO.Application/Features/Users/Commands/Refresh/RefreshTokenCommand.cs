
namespace PMO.Application.Features.Users.Commands.Refresh;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<Result<LoginUserResponse>>;

internal sealed class RefreshTokenCommandHandler(IIdentityService _identityService) : IRequestHandler<RefreshTokenCommand, Result<LoginUserResponse>>
{
    public async Task<Result<LoginUserResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var tokenData = await _identityService.RefreshTokenAsync(request.RefreshToken);
        if (tokenData == null)
            return Result<LoginUserResponse>.Failure("Invalid refresh token.");
        return Result<LoginUserResponse>.Success(tokenData);
    }
}

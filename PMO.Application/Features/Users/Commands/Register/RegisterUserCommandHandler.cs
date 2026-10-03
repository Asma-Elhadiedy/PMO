
namespace PMO.Application.Features.Users.Commands.Register;

public class RegisterUserCommandHandler(IIdentityService _identityService) : IRequestHandler<RegisterUserCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var (isSuccess, UserId) = await _identityService
            .CreateUserAsync(
                request.Email,
                request.Password,
                $"{request.FirstName} {request.LastName}", cancellationToken);

        if (!isSuccess)
            return Result<bool>.Failure("Failed to create user.");
        
        return Result<bool>.Success(true);
    }
}
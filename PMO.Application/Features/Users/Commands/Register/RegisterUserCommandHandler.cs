
namespace PMO.Application.Features.Users.Commands.Register;

public class RegisterUserCommandHandler(IIdentityService _identityService) : IRequestHandler<RegisterUserCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var (Success, UserId) = await _identityService
            .CreateUserAsync(
                request.Email,
                request.Password,
                $"{request.FirstName} {request.LastName}");

        if (!Success)
            return Result<bool>.Failure("Failed to create user.");
        
        return Result<bool>.Success(true);
    }
}
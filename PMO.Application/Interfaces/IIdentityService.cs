
namespace PMO.Application.Interfaces;

public interface IIdentityService
{
    Task<(bool isSuccess, string UserId)> CreateUserAsync(string email, string password, string fullName, CancellationToken ct);
    Task<LoginUserResponse?> AuthenticateAsync(string email, string password);
    Task<LoginUserResponse?> RefreshTokenAsync(string refreshToken);
    Task<bool> LogoutAsync(string email);
    Task<bool> UserExistsAsync(string userId);
}

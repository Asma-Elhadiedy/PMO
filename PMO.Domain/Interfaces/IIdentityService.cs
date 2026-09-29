
namespace PMO.Domain.Interfaces;

public interface IIdentityService
{
    Task<(bool Success, string UserId)> CreateUserAsync(string email, string password, string fullName, CancellationToken ct);
    Task<string?> AuthenticateAsync(string email, string password);
    Task<bool> LogoutAsync(string email);
}
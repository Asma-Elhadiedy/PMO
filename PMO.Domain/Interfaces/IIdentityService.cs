
namespace PMO.Domain.Interfaces;

public interface IIdentityService
{
    Task<(bool Success, string UserId)> CreateUserAsync(string email, string password, string fullname);
    Task<string?> AuthenticateAsync(string email, string password);
    Task<bool> LogoutAsync(string email);
}
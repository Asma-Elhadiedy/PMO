
using Microsoft.IdentityModel.Tokens;
using PMO.Domain.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PMO.Infrastructure.Services;

internal class IdentityService(UserManager<ApplicationUser> _userManager, IOptions<JWTTokenOptions> _jwtOptions) : IIdentityService
{

    public async Task<(bool Success, string UserId)> CreateUserAsync(string email, string password, string fullName)
    {
        var user = new ApplicationUser
        {
            Email = email,
            UserName = email,
            FullName = fullName
        };
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = result.Errors
                .ToDictionary(g => g.Code, g => new string[] { g.Description });
            throw new ValidationException(errors);
        }

        return (result.Succeeded, user.Id);
    }

    public async Task<string?> AuthenticateAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return null;

        var isMatch = await _userManager.CheckPasswordAsync(user, password);
        return isMatch ? GenerateToken(user) : null;
    }

    private string GenerateToken(ApplicationUser user)
    {
        var tokenOptions = _jwtOptions.Value;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecretKey));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = tokenOptions.Issuer,
            Audience = tokenOptions.Audience,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(tokenOptions.ExpirationInMinutes),
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
            ]),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public Task<bool> LogoutAsync(string email)
    {
        throw new NotImplementedException();
    }
}
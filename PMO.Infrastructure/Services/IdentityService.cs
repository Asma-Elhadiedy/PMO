
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace PMO.Infrastructure.Services;

internal class IdentityService(
    IUnitOfWork _unitOfWork, 
    SignInManager<ApplicationUser> _signInManager, 
    UserManager<ApplicationUser> _userManager, 
    IOptions<JWTTokenOptions> _jwtOptions) : IIdentityService
{
    public async Task<(bool isSuccess, string UserId)> CreateUserAsync(string email, string password, string fullName, CancellationToken ct)
    {
        var user = new ApplicationUser
        {
            Email = email,
            UserName = email,
            FullName = fullName
        };

        var transactionResult = await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .ToDictionary(g => g.Code, g => new string[] { g.Description });
                throw new ValidationException(errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, ConstRoles.User);
            if (!roleResult.Succeeded)
            {
                var errors = roleResult.Errors
                    .ToDictionary(g => g.Code, g => new string[] { g.Description });
                throw new ValidationException(errors);
            }
            return true;

        }, ct);
        return (transactionResult, user.Id);
    }

    public async Task<LoginUserResponse?> AuthenticateAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return null;

        var isMatch = await _userManager.CheckPasswordAsync(user, password);
        return isMatch ? await GenerateToken(user) : null;
    }

    public async Task<LoginUserResponse?> RefreshTokenAsync(string refreshToken)
    {
        var tokenUserId = await _unitOfWork.Repository<RefreshToken>()
            .GetItemSelectedAsync(
                t => t.UserId,
                t => t.Token == refreshToken && t.Expiration > DateTime.UtcNow);

        if (tokenUserId == null) return null;
        var user = await _userManager.FindByIdAsync(tokenUserId);
        if (user == null) return null;


        await RevokeTokens(user.Id);
        return await GenerateToken(user);
    }

    public async Task LogoutAsync(string userId)
    {
        await _signInManager.SignOutAsync();
        await RevokeTokens(userId);
        return;
    }

    async Task<int> RevokeTokens(string userId)
    {
        return await _unitOfWork.Repository<RefreshToken>()
            .BulkDeleteAsync(t => t.UserId == userId);
    }

    public async Task<bool> UserExistsAsync(string userId)
        => await _userManager.Users
            .Where(u => u.Id == userId)
            .Select(u => u.Id)
            .FirstOrDefaultAsync() != null;


    #region Helpers
    async Task<LoginUserResponse> GenerateToken(ApplicationUser user)
    {
        var tokenOptions = _jwtOptions.Value;

        var userRoles = await _userManager.GetRolesAsync(user);
        var roleClaims = userRoles
            .Select(role => new Claim(ClaimTypes.Role, role));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecretKey));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = tokenOptions.Issuer,
            Audience = tokenOptions.Audience,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(tokenOptions.ExpirationInMinutes),
            Subject = new(
            [
                new (ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.Name, user.FullName),
                ..roleClaims
            ]),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        RefreshToken refreshToken = new()
        {
            Token = GenerateRefreshToken(),
            Expiration = DateTime.UtcNow.AddDays(tokenOptions.RefreshTokenExpirationInDays),
            UserId = user.Id
        };

        _unitOfWork.Repository<RefreshToken>().Add(refreshToken);
        await _unitOfWork.CompleteAsync();

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new(tokenHandler.WriteToken(token), tokenDescriptor.Expires.Value);
    }
    static string GenerateRefreshToken()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    #endregion

}
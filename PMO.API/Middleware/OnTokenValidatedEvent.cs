
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using PMO.Infrastructure.Identity;
using System.Security.Claims;

namespace PMO.API.Middleware;

public class OnTokenValidatedEvent(UserManager<ApplicationUser> _userManager, ILogger<OnTokenValidatedEvent> _logger) : JwtBearerEvents
{
    public override async Task TokenValidated(TokenValidatedContext context)
    {
        var userIdClaim = context.Principal?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            _logger.LogWarning("User ID claim not found in token.");
            context.Fail("User ID claim not found in token.");
            return;
        }

        var user = await _userManager.FindByIdAsync(userIdClaim.Value);
        var securityClaim = context.Principal.Claims
                                .FirstOrDefault(c => c.Type == ClaimTypes.CookiePath);
        
        if (securityClaim != null && securityClaim.Value != user.SecurityStamp)
            context.Fail("Token is revoked");
    }
}
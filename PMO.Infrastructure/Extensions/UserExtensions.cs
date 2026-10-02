
using System.Security.Claims;

namespace PMO.Infrastructure.Extensions;

public static class UserExtensions
{
    extension(ClaimsPrincipal user)
    {
        public string Id => user.FindFirstValue(ClaimTypes.NameIdentifier);

    }
}

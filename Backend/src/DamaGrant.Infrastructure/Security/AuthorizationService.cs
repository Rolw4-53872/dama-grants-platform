using System.Security.Claims;

namespace DamaGrant.Infrastructure.Security;

public interface IAuthorizationService
{
    bool HasPermission(ClaimsPrincipal user, string permission);
    bool IsInRole(ClaimsPrincipal user, string role);
    int? GetUserId(ClaimsPrincipal user);
    string? GetUserEmail(ClaimsPrincipal user);
    string? GetUserRole(ClaimsPrincipal user);
}

public class AuthorizationService : IAuthorizationService
{
    public bool HasPermission(ClaimsPrincipal user, string permission)
    {
        return user.HasClaim(ClaimTypes.Role, permission);
    }

    public bool IsInRole(ClaimsPrincipal user, string role)
    {
        return user.IsInRole(role);
    }

    public int? GetUserId(ClaimsPrincipal user)
    {
        var claim = user.FindFirst("sub");
        if (claim != null && int.TryParse(claim.Value, out var userId))
        {
            return userId;
        }
        return null;
    }

    public string? GetUserEmail(ClaimsPrincipal user)
    {
        return user.FindFirst("email")?.Value;
    }

    public string? GetUserRole(ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Role)?.Value;
    }
}

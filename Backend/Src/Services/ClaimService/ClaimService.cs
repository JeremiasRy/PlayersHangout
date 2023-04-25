namespace Backend.Src.Services;

using System.Security.Claims;

public class ClaimService : IClaimService
{
    readonly private ClaimsPrincipal User;    

    public ClaimService(ClaimsPrincipal user)
    {
        User = user;        
    }

    public string GetUserIDFromToken()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        return claim != null ? claim.Value : "-1";
    }

    public ClaimsPrincipal GetClaim()
    {
        return User;
    }
}
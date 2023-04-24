using System.Security.Claims;

namespace Backend.Src.Services.ClaimService;

public class ClaimService : IClaimService
{
    private ClaimsPrincipal User;    

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
using System.Security.Claims;

namespace Aiwins.Rocket.Security.Claims {
    public interface ICurrentPrincipalAccessor {
        ClaimsPrincipal Principal { get; }
    }
}
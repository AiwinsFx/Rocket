using System.Security.Claims;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace Aiwins.Rocket.Authorization {
    public interface IRocketAuthorizationService : IAuthorizationService, IServiceProviderAccessor {
        ClaimsPrincipal CurrentPrincipal { get; }
    }
}
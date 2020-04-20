using System.Security.Claims;
using System.Threading;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Security.Claims {
    public class ThreadCurrentPrincipalAccessor : ICurrentPrincipalAccessor, ISingletonDependency {
        public virtual ClaimsPrincipal Principal => Thread.CurrentPrincipal as ClaimsPrincipal;
    }
}
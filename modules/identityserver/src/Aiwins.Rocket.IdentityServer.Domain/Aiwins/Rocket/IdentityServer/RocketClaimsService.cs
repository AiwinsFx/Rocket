using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using Aiwins.Rocket.Security.Claims;

namespace Aiwins.Rocket.IdentityServer
{
    public class RocketClaimsService : DefaultClaimsService
    {
        public RocketClaimsService(IProfileService profile, ILogger<DefaultClaimsService> logger)
            : base(profile, logger)
        {
        }

        protected override IEnumerable<Claim> GetOptionalClaims(ClaimsPrincipal subject)
        {
            var tenantClaim = subject.FindFirst(RocketClaimTypes.TenantId);
            if (tenantClaim == null)
            {
                return base.GetOptionalClaims(subject);
            }

            return base.GetOptionalClaims(subject).Union(new[] { tenantClaim });
        }
    }
}

using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aiwins.Rocket.MultiTenancy {
    public class RocketTenantResolveOptions {
        [NotNull]
        public List<ITenantResolveContributor> TenantResolvers { get; }

        public RocketTenantResolveOptions () {
            TenantResolvers = new List<ITenantResolveContributor> {
                new CurrentUserTenantResolveContributor ()
            };
        }
    }
}
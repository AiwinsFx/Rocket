using System;
using System.Linq;
using Aiwins.Rocket.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.MultiTenancy {
    public class TenantResolver : ITenantResolver, ITransientDependency {
        private readonly IServiceProvider _serviceProvider;
        private readonly RocketTenantResolveOptions _options;

        public TenantResolver (IOptions<RocketTenantResolveOptions> options, IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }

        public TenantResolveResult ResolveTenantIdOrName () {
            var result = new TenantResolveResult ();

            using (var serviceScope = _serviceProvider.CreateScope ()) {
                var context = new TenantResolveContext (serviceScope.ServiceProvider);

                foreach (var tenantResolver in _options.TenantResolvers) {
                    tenantResolver.Resolve (context);

                    result.AppliedResolvers.Add (tenantResolver.Name);

                    if (context.HasResolvedTenantOrHost ()) {
                        result.TenantIdOrName = context.TenantIdOrName;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
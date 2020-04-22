using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Autofac;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Ldap {
    [DependsOn (
        typeof (RocketAutofacModule)
    )]
    public class RocketLdapModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            var configuration = context.Services.GetConfiguration ();
            Configure<RocketLdapOptions> (configuration.GetSection ("LDAP"));
        }
    }
}
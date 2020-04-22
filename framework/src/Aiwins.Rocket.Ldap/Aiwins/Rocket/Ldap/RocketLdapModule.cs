using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Autofac;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Ldap {
    [DependsOn (
        typeof (AbpAutofacModule)
    )]
    public class AbpLdapModule : AbpModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            var configuration = context.Services.GetConfiguration ();
            Configure<AbpLdapOptions> (configuration.GetSection ("LDAP"));
        }
    }
}
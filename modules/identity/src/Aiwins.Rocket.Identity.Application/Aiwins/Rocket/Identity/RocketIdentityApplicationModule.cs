using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Identity {
    [DependsOn (
        typeof (RocketIdentityDomainModule),
        typeof (RocketIdentityApplicationContractsModule),
        typeof (RocketAutoMapperModule),
        typeof (RocketPermissionManagementApplicationModule)
    )]
    public class RocketIdentityApplicationModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddAutoMapperObjectMapper<RocketIdentityApplicationModule> ();

            Configure<RocketAutoMapperOptions> (options => {
                options.AddProfile<RocketIdentityApplicationModuleAutoMapperProfile> (validate: true);
            });
        }
    }
}
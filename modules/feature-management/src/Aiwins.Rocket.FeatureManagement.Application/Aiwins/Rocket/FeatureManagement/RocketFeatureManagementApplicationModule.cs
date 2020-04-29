using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.FeatureManagement {
    [DependsOn (
        typeof (RocketFeatureManagementDomainModule),
        typeof (RocketFeatureManagementApplicationContractsModule),
        typeof (RocketAutoMapperModule)
    )]
    public class RocketFeatureManagementApplicationModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddAutoMapperObjectMapper<RocketFeatureManagementApplicationModule> ();
            Configure<RocketAutoMapperOptions> (options => {
                options.AddProfile<FeatureManagementApplicationAutoMapperProfile> (validate: true);
            });
        }
    }
}
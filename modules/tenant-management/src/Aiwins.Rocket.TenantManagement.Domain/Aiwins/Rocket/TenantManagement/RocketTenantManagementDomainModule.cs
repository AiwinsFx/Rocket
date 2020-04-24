using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.EventBus.Distributed;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.UI;

namespace Aiwins.Rocket.TenantManagement
{
    [DependsOn(typeof(RocketMultiTenancyModule))]
    [DependsOn(typeof(RocketTenantManagementDomainSharedModule))]
    [DependsOn(typeof(RocketDataModule))]
    [DependsOn(typeof(RocketDddDomainModule))]
    [DependsOn(typeof(RocketAutoMapperModule))]
    [DependsOn(typeof(RocketUiModule))] //TODO: It's not good to depend on the UI module. However, UserFriendlyException is inside it!
    public class RocketTenantManagementDomainModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<RocketTenantManagementDomainModule>();

            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<RocketTenantManagementDomainMappingProfile>(validate: true);
            });

            Configure<RocketDistributedEventBusOptions>(options =>
            {
                options.EtoMappings.Add<Tenant, TenantEto>();
            });
        }
    }
}

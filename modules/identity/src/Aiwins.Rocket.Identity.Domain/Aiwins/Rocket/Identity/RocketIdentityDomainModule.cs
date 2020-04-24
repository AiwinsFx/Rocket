using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.EventBus.Distributed;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Users;

namespace Aiwins.Rocket.Identity
{
    [DependsOn(
        typeof(RocketDddDomainModule),
        typeof(RocketIdentityDomainSharedModule),
        typeof(RocketUsersDomainModule),
        typeof(RocketAutoMapperModule)
        )]
    public class RocketIdentityDomainModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<RocketIdentityDomainModule>();

            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<IdentityDomainMappingProfile>(validate: true);
            });

            Configure<RocketDistributedEventBusOptions>(options =>
            {
                options.EtoMappings.Add<IdentityUser, UserEto>(typeof(RocketIdentityDomainModule));
                options.EtoMappings.Add<IdentityClaimType, IdentityClaimTypeEto>(typeof(RocketIdentityDomainModule));
                options.EtoMappings.Add<IdentityRole, IdentityRoleEto>(typeof(RocketIdentityDomainModule));
            });
            
            var identityBuilder = context.Services.AddRocketIdentity(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            context.Services.AddObjectAccessor(identityBuilder);
            context.Services.ExecutePreConfiguredActions(identityBuilder);

            AddRocketIdentityOptionsFactory(context.Services);
        }

        private static void AddRocketIdentityOptionsFactory(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Transient<IOptionsFactory<IdentityOptions>, RocketIdentityOptionsFactory>());
            services.Replace(ServiceDescriptor.Scoped<IOptions<IdentityOptions>, OptionsManager<IdentityOptions>>());
        }
    }
}
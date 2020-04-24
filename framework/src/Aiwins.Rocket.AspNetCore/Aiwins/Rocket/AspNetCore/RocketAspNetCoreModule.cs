using Aiwins.Rocket.AspNetCore.Auditing;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Authorization;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.Http;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Security;
using Aiwins.Rocket.UI;
using Aiwins.Rocket.Uow;
using Aiwins.Rocket.Validation;
using Aiwins.Rocket.VirtualFileSystem;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore {
    [DependsOn (
        typeof (RocketAuditingModule),
        typeof (RocketSecurityModule),
        typeof (RocketVirtualFileSystemModule),
        typeof (RocketUnitOfWorkModule),
        typeof (RocketHttpModule),
        typeof (RocketAuthorizationModule),
        typeof (RocketDddDomainModule), //TODO: Can we remove this?
        typeof (RocketLocalizationModule),
        typeof (RocketUiModule), //TODO: Can we remove this?
        typeof (RocketValidationModule)
    )]
    public class RocketAspNetCoreModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketAuditingOptions> (options => {
                options.Contributors.Add (new AspNetCoreAuditLogContributor ());
            });

            AddAspNetServices (context.Services);
            context.Services.AddObjectAccessor<IApplicationBuilder> ();

            context.Services.Replace (ServiceDescriptor.Transient<IOptionsFactory<RequestLocalizationOptions>, RocketRequestLocalizationOptionsFactory> ());
        }

        private static void AddAspNetServices (IServiceCollection services) {
            services.AddHttpContextAccessor ();
        }
    }
}
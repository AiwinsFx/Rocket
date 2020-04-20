using System;
using System.Collections.Generic;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Authorization {
    [DependsOn (
        typeof (RocketSecurityModule),
        typeof (RocketLocalizationAbstractionsModule),
        typeof (RocketMultiTenancyModule)
    )]
    public class RocketAuthorizationModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.OnRegistred (AuthorizationInterceptorRegistrar.RegisterIfNeeded);
            AutoAddDefinitionProviders (context.Services);
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddAuthorizationCore ();

            context.Services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler> ();

            Configure<RocketPermissionOptions> (options => {
                options.ValueProviders.Add<UserPermissionValueProvider> ();
                options.ValueProviders.Add<RolePermissionValueProvider> ();
                options.ValueProviders.Add<ClientPermissionValueProvider> ();
            });
        }

        private static void AutoAddDefinitionProviders (IServiceCollection services) {
            var definitionProviders = new List<Type> ();

            services.OnRegistred (context => {
                if (typeof (IPermissionDefinitionProvider).IsAssignableFrom (context.ImplementationType)) {
                    definitionProviders.Add (context.ImplementationType);
                }
            });

            services.Configure<RocketPermissionOptions> (options => {
                options.DefinitionProviders.AddIfNotContains (definitionProviders);
            });
        }
    }
}
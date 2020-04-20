using System;
using System.Collections.Generic;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Settings {
    [DependsOn (
        typeof (RocketLocalizationAbstractionsModule),
        typeof (RocketSecurityModule),
        typeof (RocketMultiTenancyModule)
    )]
    public class RocketSettingsModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            AutoAddDefinitionProviders (context.Services);
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketSettingOptions> (options => {
                options.ValueProviders.Add<DefaultValueSettingValueProvider> ();
                options.ValueProviders.Add<ConfigurationSettingValueProvider> ();
                options.ValueProviders.Add<GlobalSettingValueProvider> ();
                options.ValueProviders.Add<TenantSettingValueProvider> ();
                options.ValueProviders.Add<UserSettingValueProvider> ();
            });
        }

        private static void AutoAddDefinitionProviders (IServiceCollection services) {
            var definitionProviders = new List<Type> ();

            services.OnRegistred (context => {
                if (typeof (ISettingDefinitionProvider).IsAssignableFrom (context.ImplementationType)) {
                    definitionProviders.Add (context.ImplementationType);
                }
            });

            services.Configure<RocketSettingOptions> (options => {
                options.DefinitionProviders.AddIfNotContains (definitionProviders);
            });
        }
    }
}
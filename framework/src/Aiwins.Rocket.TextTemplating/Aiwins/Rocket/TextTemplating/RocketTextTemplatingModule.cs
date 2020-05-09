using System;
using System.Collections.Generic;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.TextTemplating {
    [DependsOn (
        typeof (RocketVirtualFileSystemModule),
        typeof (RocketLocalizationAbstractionsModule)
    )]
    public class RocketTextTemplatingModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            AutoAddProvidersAndContributors (context.Services);
        }

        private static void AutoAddProvidersAndContributors (IServiceCollection services) {
            var definitionProviders = new List<Type> ();
            var contentContributors = new List<Type> ();

            services.OnRegistred (context => {
                if (typeof (ITemplateDefinitionProvider).IsAssignableFrom (context.ImplementationType)) {
                    definitionProviders.Add (context.ImplementationType);
                }

                if (typeof (ITemplateContentContributor).IsAssignableFrom (context.ImplementationType)) {
                    contentContributors.Add (context.ImplementationType);
                }
            });

            services.Configure<RocketTextTemplatingOptions> (options => {
                options.DefinitionProviders.AddIfNotContains (definitionProviders);
                options.ContentContributors.AddIfNotContains (contentContributors);
            });
        }
    }
}
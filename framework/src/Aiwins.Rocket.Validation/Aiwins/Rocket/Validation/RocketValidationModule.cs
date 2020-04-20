using System;
using System.Collections.Generic;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Validation.Localization;
using Aiwins.Rocket.VirtualFileSystem;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Validation {
    [DependsOn (typeof (RocketLocalizationModule))]
    public class RocketValidationModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.OnRegistred (ValidationInterceptorRegistrar.RegisterIfNeeded);
            AutoAddObjectValidationContributors (context.Services);
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<RocketValidationResource> ();
            });

            Configure<RocketLocalizationOptions> (options => {
                options.Resources
                    .Add<RocketValidationResource> ("en")
                    .AddVirtualJson ("/Aiwins/Rocket/Validation/Localization");
            });
        }

        private static void AutoAddObjectValidationContributors (IServiceCollection services) {
            var contributorTypes = new List<Type> ();

            services.OnRegistred (context => {
                if (typeof (IObjectValidationContributor).IsAssignableFrom (context.ImplementationType)) {
                    contributorTypes.Add (context.ImplementationType);
                }
            });

            services.Configure<RocketValidationOptions> (options => {
                options.ObjectValidationContributors.AddIfNotContains (contributorTypes);
            });
        }
    }
}
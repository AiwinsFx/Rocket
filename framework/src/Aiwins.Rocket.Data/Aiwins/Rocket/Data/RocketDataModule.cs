using System;
using System.Collections.Generic;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.ObjectExtending;
using Aiwins.Rocket.Uow;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Data {
    [DependsOn (
        typeof (RocketObjectExtendingModule),
        typeof (RocketUnitOfWorkModule)
    )]
    public class RocketDataModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            AutoAddDataSeedContributors (context.Services);
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            var configuration = context.Services.GetConfiguration ();

            Configure<RocketDbConnectionOptions> (configuration);

            context.Services.AddSingleton (typeof (IDataFilter<>), typeof (DataFilter<>));
        }

        private static void AutoAddDataSeedContributors (IServiceCollection services) {
            var contributors = new List<Type> ();

            services.OnRegistred (context => {
                if (typeof (IDataSeedContributor).IsAssignableFrom (context.ImplementationType)) {
                    contributors.Add (context.ImplementationType);
                }
            });

            services.Configure<RocketDataSeedOptions> (options => {
                options.Contributors.AddIfNotContains (contributors);
            });
        }
    }
}
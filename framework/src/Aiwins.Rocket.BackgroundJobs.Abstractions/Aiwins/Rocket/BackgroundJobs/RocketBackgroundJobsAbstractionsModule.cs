using System;
using System.Collections.Generic;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.BackgroundJobs {
    [DependsOn (
        typeof (RocketJsonModule)
    )]
    public class RocketBackgroundJobsAbstractionsModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            RegisterJobs (context.Services);
        }

        private static void RegisterJobs (IServiceCollection services) {
            var jobTypes = new List<Type> ();

            services.OnRegistred (context => {
                if (ReflectionHelper.IsAssignableToGenericType (context.ImplementationType, typeof (IBackgroundJob<>)) ||
                    ReflectionHelper.IsAssignableToGenericType (context.ImplementationType, typeof (IAsyncBackgroundJob<>))) {
                    jobTypes.Add (context.ImplementationType);
                }
            });

            services.Configure<RocketBackgroundJobOptions> (options => {
                foreach (var jobType in jobTypes) {
                    options.AddJob (jobType);
                }
            });
        }
    }
}
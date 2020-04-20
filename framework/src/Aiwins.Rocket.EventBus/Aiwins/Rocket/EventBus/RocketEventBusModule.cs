using System;
using System.Collections.Generic;
using Aiwins.Rocket.EventBus.Distributed;
using Aiwins.Rocket.EventBus.Local;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.EventBus {
    public class RocketEventBusModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            AddEventHandlers (context.Services);
        }

        private static void AddEventHandlers (IServiceCollection services) {
            var localHandlers = new List<Type> ();
            var distributedHandlers = new List<Type> ();

            services.OnRegistred (context => {
                if (ReflectionHelper.IsAssignableToGenericType (context.ImplementationType, typeof (ILocalEventHandler<>))) {
                    localHandlers.Add (context.ImplementationType);
                } else if (ReflectionHelper.IsAssignableToGenericType (context.ImplementationType, typeof (IDistributedEventHandler<>))) {
                    distributedHandlers.Add (context.ImplementationType);
                }
            });

            services.Configure<RocketLocalEventBusOptions> (options => {
                options.Handlers.AddIfNotContains (localHandlers);
            });

            services.Configure<RocketDistributedEventBusOptions> (options => {
                options.Handlers.AddIfNotContains (distributedHandlers);
            });
        }
    }
}
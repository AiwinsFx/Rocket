using System;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.BackgroundWorkers.Quartz {
    public class RocketQuartzConventionalRegistrar : DefaultConventionalRegistrar {
        public override void AddType (IServiceCollection services, Type type) {
            if (!typeof (IQuartzBackgroundWorker).IsAssignableFrom (type)) {
                return;
            }

            var dependencyAttribute = GetDependencyAttributeOrNull (type);
            var lifeTime = GetLifeTimeOrNull (type, dependencyAttribute);

            if (lifeTime == null) {
                return;
            }

            services.Add (ServiceDescriptor.Describe (typeof (IQuartzBackgroundWorker), type, lifeTime.Value));
        }
    }
}
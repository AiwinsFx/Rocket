using System;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Aiwins.Rocket.DependencyInjection {
    //TODO: 考虑DefaultConventionalRegistrar更易扩展!
    public class DefaultConventionalRegistrar : ConventionalRegistrarBase {
        public override void AddType (IServiceCollection services, Type type) {
            if (IsConventionalRegistrationDisabled (type)) {
                return;
            }

            var dependencyAttribute = GetDependencyAttributeOrNull (type);
            var lifeTime = GetLifeTimeOrNull (type, dependencyAttribute);

            if (lifeTime == null) {
                return;
            }

            var serviceTypes = ExposedServiceExplorer.GetExposedServices (type);

            TriggerServiceExposing (services, type, serviceTypes);

            foreach (var serviceType in serviceTypes) {
                var serviceDescriptor = ServiceDescriptor.Describe (serviceType, type, lifeTime.Value);

                if (dependencyAttribute?.ReplaceServices == true) {
                    services.Replace (serviceDescriptor);
                } else if (dependencyAttribute?.TryRegister == true) {
                    services.TryAdd (serviceDescriptor);
                } else {
                    services.Add (serviceDescriptor);
                }
            }
        }

        protected virtual DependencyAttribute GetDependencyAttributeOrNull (Type type) {
            return type.GetCustomAttribute<DependencyAttribute> (true);
        }

        protected virtual ServiceLifetime? GetLifeTimeOrNull (Type type, [CanBeNull] DependencyAttribute dependencyAttribute) {
            return dependencyAttribute?.Lifetime ?? GetServiceLifetimeFromClassHierarcy (type);
        }

        protected virtual ServiceLifetime? GetServiceLifetimeFromClassHierarcy (Type type) {
            if (typeof (ITransientDependency).GetTypeInfo ().IsAssignableFrom (type)) {
                return ServiceLifetime.Transient;
            }

            if (typeof (ISingletonDependency).GetTypeInfo ().IsAssignableFrom (type)) {
                return ServiceLifetime.Singleton;
            }

            if (typeof (IScopedDependency).GetTypeInfo ().IsAssignableFrom (type)) {
                return ServiceLifetime.Scoped;
            }

            return null;
        }
    }
}
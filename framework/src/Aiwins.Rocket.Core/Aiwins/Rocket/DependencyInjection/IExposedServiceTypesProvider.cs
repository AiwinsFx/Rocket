using System;

namespace Aiwins.Rocket.DependencyInjection {
    public interface IExposedServiceTypesProvider {
        Type[] GetExposedServiceTypes (Type targetType);
    }
}
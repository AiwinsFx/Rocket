using System;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.ObjectMapping {
    public sealed class NotImplementedAutoObjectMappingProvider : IAutoObjectMappingProvider, ISingletonDependency {
        public TDestination Map<TSource, TDestination> (object source) {
            throw new NotImplementedException ($"Can not map from given object ({source}) to {typeof(TDestination).AssemblyQualifiedName}.");
        }

        public TDestination Map<TSource, TDestination> (TSource source, TDestination destination) {
            throw new NotImplementedException ($"Can no map from {typeof(TSource).AssemblyQualifiedName} to {typeof(TDestination).AssemblyQualifiedName}.");
        }
    }
}
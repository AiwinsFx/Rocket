using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Reflection;

namespace Aiwins.Rocket.Serialization {
    public class RocketSerializationModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.OnExposing (onServiceExposingContext => {
                //Register types for IObjectSerializer<T> if implements
                onServiceExposingContext.ExposedTypes.AddRange (
                    ReflectionHelper.GetImplementedGenericTypes (
                        onServiceExposingContext.ImplementationType,
                        typeof (IObjectSerializer<>)
                    )
                );
            });
        }
    }
}
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.ObjectMapping {
    public class RocketObjectMappingModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.OnExposing (onServiceExposingContext => {
                //自动注册实现了IObjectMapper<TSource, TDestination>接口的类型
                onServiceExposingContext.ExposedTypes.AddRange (
                    ReflectionHelper.GetImplementedGenericTypes (
                        onServiceExposingContext.ImplementationType,
                        typeof (IObjectMapper<,>)
                    )
                );
            });
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddTransient (
                typeof (IObjectMapper<>),
                typeof (DefaultObjectMapper<>)
            );
        }
    }
}
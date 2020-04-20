using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.ObjectMapping;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection {
    public static class RocketAutoMapperServiceCollectionExtensions {
        public static IServiceCollection AddAutoMapperObjectMapper (this IServiceCollection services) {
            return services.Replace (
                ServiceDescriptor.Transient<IAutoObjectMappingProvider, AutoMapperAutoObjectMappingProvider> ()
            );
        }

        public static IServiceCollection AddAutoMapperObjectMapper<TContext> (this IServiceCollection services) {
            return services.Replace (
                ServiceDescriptor.Transient<IAutoObjectMappingProvider<TContext>, AutoMapperAutoObjectMappingProvider<TContext>> ()
            );
        }
    }
}
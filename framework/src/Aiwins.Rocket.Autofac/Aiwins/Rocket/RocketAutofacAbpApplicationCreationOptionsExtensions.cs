using Aiwins.Rocket.Autofac;
using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket {
    public static class RocketAutofacRocketApplicationCreationOptionsExtensions {
        public static void UseAutofac (this RocketApplicationCreationOptions options) {
            var builder = new ContainerBuilder ();
            options.Services.AddObjectAccessor (builder);
            options.Services.AddSingleton ((IServiceProviderFactory<ContainerBuilder>) new RocketAutofacServiceProviderFactory (builder));
        }
    }
}
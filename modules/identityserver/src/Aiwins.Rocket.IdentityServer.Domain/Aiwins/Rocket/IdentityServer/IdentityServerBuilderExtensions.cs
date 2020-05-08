using Aiwins.Rocket.IdentityServer.Clients;
using Aiwins.Rocket.IdentityServer.Devices;
using Aiwins.Rocket.IdentityServer.Grants;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.IdentityServer {
    public static class IdentityServerBuilderExtensions {
        public static IIdentityServerBuilder AddRocketStores (this IIdentityServerBuilder builder) {
            builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStore> ();
            builder.Services.AddTransient<IDeviceFlowStore, DeviceFlowStore> ();

            return builder
                .AddClientStore<ClientStore> ()
                .AddResourceStore<ResourceStore> ()
                .AddCorsPolicyService<RocketCorsPolicyService> ();
        }
    }
}
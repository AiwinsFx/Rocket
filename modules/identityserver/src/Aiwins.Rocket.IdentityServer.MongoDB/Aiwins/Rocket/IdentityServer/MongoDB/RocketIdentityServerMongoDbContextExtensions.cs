using System;
using Aiwins.Rocket.IdentityServer.ApiResources;
using Aiwins.Rocket.IdentityServer.Clients;
using Aiwins.Rocket.IdentityServer.Devices;
using Aiwins.Rocket.IdentityServer.Grants;
using Aiwins.Rocket.IdentityServer.IdentityResources;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.IdentityServer.MongoDB {
    public static class RocketIdentityServerMongoDbContextExtensions {
        public static void ConfigureIdentityServer (
            this IMongoModelBuilder builder,
            Action<IdentityServerMongoModelBuilderConfigurationOptions> optionsAction = null) {
            Check.NotNull (builder, nameof (builder));

            var options = new IdentityServerMongoModelBuilderConfigurationOptions (
                RocketIdentityServerDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke (options);

            builder.Entity<ApiResource> (b => {
                b.CollectionName = options.CollectionPrefix + "ApiResources";
            });

            builder.Entity<Client> (b => {
                b.CollectionName = options.CollectionPrefix + "Clients";
            });
            builder.Entity<IdentityResource> (b => {
                b.CollectionName = options.CollectionPrefix + "IdentityResources";
            });

            builder.Entity<PersistedGrant> (b => {
                b.CollectionName = options.CollectionPrefix + "PersistedGrants";
            });

            builder.Entity<DeviceFlowCodes> (b => {
                b.CollectionName = options.CollectionPrefix + "DeviceFlowCodes";
            });
        }
    }
}
using System;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.TenantManagement.MongoDB {
    public static class RocketTenantManagementMongoDbContextExtensions {
        public static void ConfigureTenantManagement (
            this IMongoModelBuilder builder,
            Action<RocketMongoModelBuilderConfigurationOptions> optionsAction = null) {
            Check.NotNull (builder, nameof (builder));

            var options = new TenantManagementMongoModelBuilderConfigurationOptions (
                RocketTenantManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke (options);

            builder.Entity<Tenant> (b => {
                b.CollectionName = options.CollectionPrefix + "Tenants";
            });
        }
    }
}
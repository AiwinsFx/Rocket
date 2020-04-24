using System;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.PermissionManagement.MongoDB
{
    public static class RocketPermissionManagementMongoDbContextExtensions
    {
        public static void ConfigurePermissionManagement(
            this IMongoModelBuilder builder,
            Action<RocketMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new PermissionManagementMongoModelBuilderConfigurationOptions(
                RocketPermissionManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);

            builder.Entity<PermissionGrant>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "PermissionGrants";
            });
        }
    }
}
using System;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.FeatureManagement.MongoDB {
    public static class FeatureManagementMongoDbContextExtensions {
        public static void ConfigureFeatureManagement (
            this IMongoModelBuilder builder,
            Action<RocketMongoModelBuilderConfigurationOptions> optionsAction = null) {
            Check.NotNull (builder, nameof (builder));

            var options = new FeatureManagementMongoModelBuilderConfigurationOptions (
                FeatureManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke (options);

            builder.Entity<FeatureValue> (b => {
                b.CollectionName = options.CollectionPrefix + "FeatureValues";
            });
        }
    }
}
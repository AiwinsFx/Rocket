using System;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.SettingManagement.MongoDB
{
    public static class SettingManagementMongoDbContextExtensions
    {
        public static void ConfigureSettingManagement(
            this IMongoModelBuilder builder,
            Action<SettingManagementMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new SettingManagementMongoModelBuilderConfigurationOptions(
                RocketSettingManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);

            builder.Entity<Setting>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "Settings";
            });
        }
    }
}
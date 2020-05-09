using System;
using Aiwins.Rocket;
using Aiwins.Rocket.MongoDB;

namespace MyCompanyName.MyProjectName.MongoDB
{
    public static class MyProjectNameMongoDbContextExtensions
    {
        public static void ConfigureMyProjectName(
            this IMongoModelBuilder builder,
            Action<RocketMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new MyProjectNameMongoModelBuilderConfigurationOptions(
                MyProjectNameDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}
using System;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.Identity.MongoDB {
    public static class RocketIdentityMongoDbContextExtensions {
        public static void ConfigureIdentity (
            this IMongoModelBuilder builder,
            Action<IdentityMongoModelBuilderConfigurationOptions> optionsAction = null) {
            Check.NotNull (builder, nameof (builder));

            var options = new IdentityMongoModelBuilderConfigurationOptions (
                RocketIdentityDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke (options);

            builder.Entity<IdentityUser> (b => {
                b.CollectionName = options.CollectionPrefix + "Users";
            });

            builder.Entity<IdentityRole> (b => {
                b.CollectionName = options.CollectionPrefix + "Roles";
            });

            builder.Entity<IdentityClaimType> (b => {
                b.CollectionName = options.CollectionPrefix + "ClaimTypes";
            });
        }
    }
}
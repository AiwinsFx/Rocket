using System;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.AuditLogging.MongoDB {
    public static class RocketAuditLoggingMongoDbContextExtensions {
        public static void ConfigureAuditLogging (
            this IMongoModelBuilder builder,
            Action<AuditLoggingMongoModelBuilderConfigurationOptions> optionsAction = null) {
            Check.NotNull (builder, nameof (builder));

            var options = new AuditLoggingMongoModelBuilderConfigurationOptions (
                RocketAuditLoggingDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke (options);

            builder.Entity<AuditLog> (b => {
                b.CollectionName = options.CollectionPrefix + "AuditLogs";
            });
        }
    }
}
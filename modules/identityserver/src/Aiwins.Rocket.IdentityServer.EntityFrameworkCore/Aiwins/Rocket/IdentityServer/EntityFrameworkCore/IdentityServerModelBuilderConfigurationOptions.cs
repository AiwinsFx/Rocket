using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore.Modeling;
using JetBrains.Annotations;

namespace Aiwins.Rocket.IdentityServer.EntityFrameworkCore {
    public class IdentityServerModelBuilderConfigurationOptions : RocketModelBuilderConfigurationOptions {
        public EfCoreDatabaseProvider? DatabaseProvider { get; set; }

        public IdentityServerModelBuilderConfigurationOptions (
            [NotNull] string tablePrefix, [CanBeNull] string schema) : base (
            tablePrefix,
            schema) {

        }
    }
}
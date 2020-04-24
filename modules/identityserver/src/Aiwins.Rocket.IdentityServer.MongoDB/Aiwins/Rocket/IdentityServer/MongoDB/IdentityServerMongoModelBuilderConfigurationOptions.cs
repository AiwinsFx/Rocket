using JetBrains.Annotations;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.IdentityServer.MongoDB
{
    public class IdentityServerMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions
    {
        public IdentityServerMongoModelBuilderConfigurationOptions([NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}

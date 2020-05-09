using JetBrains.Annotations;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Docs.MongoDB
{
    public class DocsMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions
    {
        public DocsMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix)
            : base(collectionPrefix)
        {
        }
    }
}

using JetBrains.Annotations;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Blogging.MongoDB
{
    public class BloggingMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions
    {
        public BloggingMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}

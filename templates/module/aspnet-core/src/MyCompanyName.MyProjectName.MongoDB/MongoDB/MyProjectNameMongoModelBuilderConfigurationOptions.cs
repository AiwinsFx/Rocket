using JetBrains.Annotations;
using Aiwins.Rocket.MongoDB;

namespace MyCompanyName.MyProjectName.MongoDB
{
    public class MyProjectNameMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions
    {
        public MyProjectNameMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}
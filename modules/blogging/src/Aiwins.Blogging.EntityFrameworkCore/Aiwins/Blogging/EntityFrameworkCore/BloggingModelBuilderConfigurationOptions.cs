using JetBrains.Annotations;
using Aiwins.Rocket.EntityFrameworkCore.Modeling;

namespace Aiwins.Blogging.EntityFrameworkCore
{
    public class BloggingModelBuilderConfigurationOptions : RocketModelBuilderConfigurationOptions
    {
        public BloggingModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(tablePrefix, schema)
        {
        }
    }
}
using JetBrains.Annotations;
using Aiwins.Rocket.EntityFrameworkCore.Modeling;

namespace Aiwins.Docs.EntityFrameworkCore
{
    public class DocsModelBuilderConfigurationOptions : RocketModelBuilderConfigurationOptions
    {
        public DocsModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix,
            [CanBeNull] string schema)
            : base(tablePrefix, schema)
        {
        }
    }
}
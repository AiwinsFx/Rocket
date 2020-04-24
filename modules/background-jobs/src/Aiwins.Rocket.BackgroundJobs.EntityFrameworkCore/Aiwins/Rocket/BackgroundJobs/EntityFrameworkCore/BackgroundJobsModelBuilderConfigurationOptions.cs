using JetBrains.Annotations;
using Aiwins.Rocket.EntityFrameworkCore.Modeling;

namespace Aiwins.Rocket.BackgroundJobs.EntityFrameworkCore
{
    public class BackgroundJobsModelBuilderConfigurationOptions : RocketModelBuilderConfigurationOptions
    {
        public BackgroundJobsModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}
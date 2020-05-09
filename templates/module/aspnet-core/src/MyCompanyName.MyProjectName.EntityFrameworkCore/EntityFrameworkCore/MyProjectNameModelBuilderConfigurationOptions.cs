using JetBrains.Annotations;
using Aiwins.Rocket.EntityFrameworkCore.Modeling;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore
{
    public class MyProjectNameModelBuilderConfigurationOptions : RocketModelBuilderConfigurationOptions
    {
        public MyProjectNameModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}
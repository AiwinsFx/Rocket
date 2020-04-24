using JetBrains.Annotations;
using Aiwins.Rocket.EntityFrameworkCore.Modeling;

namespace Aiwins.Rocket.TenantManagement.EntityFrameworkCore
{
    public class RocketTenantManagementModelBuilderConfigurationOptions : RocketModelBuilderConfigurationOptions
    {
        public RocketTenantManagementModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix,
            [CanBeNull] string schema)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}
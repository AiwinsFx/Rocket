using System.Threading.Tasks;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Identity
{
    public class IdentityDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        protected IIdentityDataSeeder IdentityDataSeeder { get; }

        public IdentityDataSeedContributor(IIdentityDataSeeder identityDataSeeder)
        {
            IdentityDataSeeder = identityDataSeeder;
        }

        public virtual Task SeedAsync(DataSeedContext context)
        {
            return IdentityDataSeeder.SeedAsync(
                context["AdminEmail"] as string ?? "admin@rocket.io",
                context["AdminPassword"] as string ?? "1q2w3E*",
                context.TenantId
            );
        }
    }
}

using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Identity
{
    public interface IIdentityDataSeeder
    {
        Task<IdentityDataSeedResult> SeedAsync(
            [NotNull] string adminPhoneNumber,
            [NotNull] string adminPassword,
            Guid? tenantId = null);
    }
}
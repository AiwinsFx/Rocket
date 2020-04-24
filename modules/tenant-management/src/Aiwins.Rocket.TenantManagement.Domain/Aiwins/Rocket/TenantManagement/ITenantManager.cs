using System.Threading.Tasks;
using JetBrains.Annotations;
using Aiwins.Rocket.Domain.Services;

namespace Aiwins.Rocket.TenantManagement
{
    public interface ITenantManager : IDomainService
    {
        [NotNull]
        Task<Tenant> CreateAsync([NotNull] string name);

        Task ChangeNameAsync([NotNull] Tenant tenant, [NotNull] string name);
    }
}

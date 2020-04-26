using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Services;
using JetBrains.Annotations;

namespace Aiwins.Rocket.TenantManagement {
    public interface ITenantManager : IDomainService {
        [NotNull]
        Task<Tenant> CreateAsync ([NotNull] string name);

        Task ChangeNameAsync ([NotNull] Tenant tenant, [NotNull] string name);
    }
}
using Aiwins.Rocket.DependencyInjection;
using JetBrains.Annotations;

namespace Aiwins.Rocket.MultiTenancy {
    public interface ITenantResolveContext : IServiceProviderAccessor {
        [CanBeNull]
        string TenantIdOrName { get; set; }

        bool Handled { get; set; }
    }
}
using JetBrains.Annotations;

namespace Aiwins.Rocket.MultiTenancy {
    public interface ITenantResolveResultAccessor {
        [CanBeNull]
        TenantResolveResult Result { get; set; }
    }
}
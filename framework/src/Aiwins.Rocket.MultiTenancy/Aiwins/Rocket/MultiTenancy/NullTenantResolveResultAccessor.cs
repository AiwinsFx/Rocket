using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.MultiTenancy {
    public class NullTenantResolveResultAccessor : ITenantResolveResultAccessor, ISingletonDependency {
        public TenantResolveResult Result {
            get => null;
            set { }
        }
    }
}
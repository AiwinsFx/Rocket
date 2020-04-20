namespace Aiwins.Rocket.MultiTenancy {
    public abstract class TenantResolveContributorBase : ITenantResolveContributor {
        public abstract string Name { get; }

        //TODO: 考虑异步处理
        public abstract void Resolve (ITenantResolveContext context);
    }
}
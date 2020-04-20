using JetBrains.Annotations;

namespace Aiwins.Rocket.MultiTenancy {
    public interface ITenantResolver {
        /// <summary>
        /// 通过方法 <see cref="ITenantResolveContributor"/> 解析租户信息。
        /// </summary>
        /// <returns>
        /// 租户标识、唯一名称或者空值 (未解析出租户信息的情况下)
        /// </returns>
        [NotNull]
        TenantResolveResult ResolveTenantIdOrName ();
    }
}
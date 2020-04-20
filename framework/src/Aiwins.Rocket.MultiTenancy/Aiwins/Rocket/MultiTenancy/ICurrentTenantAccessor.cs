namespace Aiwins.Rocket.MultiTenancy {
    /* 如果Current为空表明我们没有设置租户；
     * Current.TenantId为空表明我们显式设置了租户的标识为空；
     * Current.TenantId不为空表明我们已经显式设置了租户的标识。
     */

    public interface ICurrentTenantAccessor {
        BasicTenantInfo Current { get; set; }
    }
}
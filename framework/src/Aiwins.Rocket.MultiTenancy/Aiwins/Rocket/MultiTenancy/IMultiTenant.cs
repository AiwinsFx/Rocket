using System;

namespace Aiwins.Rocket.MultiTenancy {
    public interface IMultiTenant {
        /// <summary>
        /// 租户标识.
        /// </summary>
        Guid? TenantId { get; }
    }
}
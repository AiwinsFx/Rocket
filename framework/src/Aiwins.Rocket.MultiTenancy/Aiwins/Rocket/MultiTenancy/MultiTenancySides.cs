using System;

namespace Aiwins.Rocket.MultiTenancy {
    /// <summary>
    /// 租户类别
    /// </summary>
    [Flags]
    public enum MultiTenancySides {
        /// <summary>
        /// 租户
        /// </summary>
        Tenant = 1,

        /// <summary>
        /// 租主
        /// </summary>
        Host = 2,

        /// <summary>
        /// 二者皆可
        /// </summary>
        Both = Tenant | Host
    }
}
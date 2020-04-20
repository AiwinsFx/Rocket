using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.MultiTenancy {
    public class BasicTenantInfo {
        /// <summary>
        /// 值为空表示租主
        /// 不为空表示租户
        /// </summary>
        [CanBeNull]
        public Guid? TenantId { get; }

        /// <summary>
        /// 租户名称 <see cref="TenantId"/>
        /// </summary>
        [CanBeNull]
        public string Name { get; }

        public BasicTenantInfo (Guid? tenantId, string name = null) {
            TenantId = tenantId;
            Name = name;
        }
    }
}
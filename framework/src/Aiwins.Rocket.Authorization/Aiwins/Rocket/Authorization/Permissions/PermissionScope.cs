using System;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class PermissionScope {
        public string Name { get; }
        public string DisplayName { get; }
        public MultiTenancySides MultiTenancySide { get; set; }

        protected internal PermissionScope (
            string name,
            string displayName,
            MultiTenancySides multiTenancySide = MultiTenancySides.Both) {
            Name = name;
            DisplayName = displayName;
            MultiTenancySide = multiTenancySide;
        }

        public override string ToString () {
            return $"[{nameof(PermissionScope)}:{Name}:{DisplayName}:{MultiTenancySide}]";
        }
    }

    [Flags]
    public enum PermissionScopeType {
        Prohibited = 0,     // 禁止
        Owner = 1,          // 个人
        Domain = 2,         // 管辖
        Area = 4,           // 区域
        All = 8,            // 全部
        Granted = Owner | Domain | Area | All // 允许
    }
}
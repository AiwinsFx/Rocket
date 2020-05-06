using System;
using Aiwins.Rocket.MultiTenancy;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class PermissionScope {
        public string Name { get; }
        public string DisplayName { get; }
        public MultiTenancySides MultiTenancySide { get; set; }

        protected internal PermissionScope (
            [NotNull] string name,
            [NotNull] string displayName,
            MultiTenancySides multiTenancySide = MultiTenancySides.Both) {
            Check.NotNull (name, nameof (name));
            Check.NotNull (displayName, nameof (displayName));
            
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
        Prohibited = 0, // 禁止
        Owner = 1, // 个人
        Domain = 2, // 管辖
        All = 4, // 全部
        Platform = 8, // 平台
        Granted = Owner | Domain | All | Platform // 允许
    }
}
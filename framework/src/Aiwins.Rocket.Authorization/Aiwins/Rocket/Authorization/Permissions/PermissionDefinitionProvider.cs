using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.Authorization.Permissions {
    public abstract class PermissionDefinitionProvider : IPermissionDefinitionProvider, ITransientDependency {
        public static PermissionScope Prohibited => new PermissionScope (nameof (PermissionScopeType.Prohibited), "禁止");
        public static PermissionScope Platform => new PermissionScope (nameof (PermissionScopeType.Platform), "平台", MultiTenancySides.Host);
        public static PermissionScope All => new PermissionScope (nameof (PermissionScopeType.All), "全部");
        public static PermissionScope Domain => new PermissionScope (nameof (PermissionScopeType.Domain), "管辖");
        public static PermissionScope Owner => new PermissionScope (nameof (PermissionScopeType.Owner), "个人");
        public static PermissionScope Granted => new PermissionScope (nameof (PermissionScopeType.Granted), "允许");
        public virtual void PreDefine (IPermissionDefinitionContext context) {}
        public abstract void Define (IPermissionDefinitionContext context);
        public virtual void PostDefine (IPermissionDefinitionContext context) {}
    }
}
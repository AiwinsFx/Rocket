using JetBrains.Annotations;

namespace Aiwins.Rocket.Authorization.Permissions {
    public static class PermissionDefinitionContextExtensions {
        /// <summary>
        /// 通过指定的权限名称 <paramref name="name"/> 设置权限为禁用状态
        /// 未查询到权限则返回false
        /// </summary>
        /// <param name="context">权限上下文</param>
        /// <param name="name">权限名称</param>
        /// <returns>
        /// 如果权限禁用成功返回true
        /// 未查询到权限则返回false
        /// </returns>
        public static bool TryDisablePermission (
            [NotNull] this IPermissionDefinitionContext context, [NotNull] string name) {
            Check.NotNull (context, nameof (context));
            Check.NotNull (name, nameof (name));

            var permission = context.GetPermissionOrNull (name);
            if (permission == null) {
                return false;
            }

            permission.IsEnabled = false;
            return true;
        }
    }
}
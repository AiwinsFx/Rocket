using System;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.MultiTenancy;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Authorization.Permissions {
    public interface IPermissionDefinitionContext {
        //TODO: 添加查询和修改权限或权限组的方法

        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 获取预定义的权限组
        /// 未查询到相关信息抛出异常 <see cref="RocketException"/>
        /// </summary>
        /// <param name="name">Name of the group</param>
        /// <returns></returns>
        PermissionGroupDefinition GetGroup ([NotNull] string name);

        /// <summary>
        /// 获取预定义的权限组，未查询到相关信息则返回null
        /// </summary>
        /// <param name="name">权限组名称</param>
        /// <returns></returns>
        [NotNull]
        PermissionGroupDefinition GetGroupOrNull (string name);

        [CanBeNull]
        PermissionGroupDefinition AddGroup (
            [NotNull] string name,
            ILocalizableString displayName = null,
            MultiTenancySides multiTenancySide = MultiTenancySides.Both);

        void RemoveGroup (string name);

        [CanBeNull]
        PermissionDefinition GetPermissionOrNull ([NotNull] string name);
    }
}
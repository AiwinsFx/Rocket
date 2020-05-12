using System.Collections.Generic;
using System.Collections.Immutable;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.MultiTenancy;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class PermissionGroupDefinition //TODO: 考虑添加子权限组
    {
        /// <summary>
        /// 权限组名称（唯一性）
        /// </summary>
        public string Name { get; }

        public Dictionary<string, object> Properties { get; }

        public ILocalizableString DisplayName {
            get => _displayName;
            set => _displayName = Check.NotNull (value, nameof (value));
        }
        private ILocalizableString _displayName;

        /// <summary>
        /// 适用的租户类型
        /// 默认值: <see cref="MultiTenancySides.Both"/>
        /// </summary>
        public MultiTenancySides MultiTenancySide { get; set; }

        public IReadOnlyList<PermissionDefinition> Permissions => _permissions.ToImmutableList ();
        private readonly List<PermissionDefinition> _permissions;

        /// <summary>
        /// 自定义属性 <see cref="Properties"/> 索引器
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <returns>
        /// 返回自定义属性字典 <see cref="Properties"/> 中的值
        /// 如果传入的名称 <see cref="name"/> 在自定义属性 <see cref="Properties"/> 字典不存在，则返回null。
        /// </returns>
        public object this [string name] {
            get => Properties.GetOrDefault (name);
            set => Properties[name] = value;
        }

        protected internal PermissionGroupDefinition (
            string name,
            ILocalizableString displayName = null,
            MultiTenancySides multiTenancySide = MultiTenancySides.Both) {
            Name = name;
            DisplayName = displayName ?? new FixedLocalizableString (Name);
            MultiTenancySide = multiTenancySide;

            Properties = new Dictionary<string, object> ();
            _permissions = new List<PermissionDefinition> ();
        }

        public virtual PermissionDefinition AddPermission (
            string name,
            ILocalizableString displayName = null,
            MultiTenancySides multiTenancySide = MultiTenancySides.Both,
            bool isDropdownBox = true,
            bool isEnabled = true) {
            var permission = new PermissionDefinition (
                name,
                displayName,
                multiTenancySide,
                isDropdownBox,
                isEnabled
            );

            _permissions.Add (permission);

            return permission;
        }

        public virtual List<PermissionDefinition> GetPermissionsWithChildren () {
            var permissions = new List<PermissionDefinition> ();

            foreach (var permission in _permissions) {
                AddPermissionToListRecursively (permissions, permission);
            }

            return permissions;
        }

        private void AddPermissionToListRecursively (List<PermissionDefinition> permissions, PermissionDefinition permission) {
            permissions.Add (permission);

            foreach (var child in permission.Children) {
                AddPermissionToListRecursively (permissions, child);
            }
        }

        public override string ToString () {
            return $"[{nameof(PermissionGroupDefinition)} {Name}]";
        }

        [CanBeNull]
        public PermissionDefinition GetPermissionOrNull ([NotNull] string name) {
            Check.NotNull (name, nameof (name));

            return GetPermissionOrNullRecursively (Permissions, name);
        }

        private PermissionDefinition GetPermissionOrNullRecursively (
            IReadOnlyList<PermissionDefinition> permissions, string name) {
            foreach (var permission in permissions) {
                if (permission.Name == name) {
                    return permission;
                }

                var childPermission = GetPermissionOrNullRecursively (permission.Children, name);
                if (childPermission != null) {
                    return childPermission;
                }
            }

            return null;
        }
    }
}
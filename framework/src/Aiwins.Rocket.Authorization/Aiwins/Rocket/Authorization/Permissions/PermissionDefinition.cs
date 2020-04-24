using System.Collections.Generic;
using System.Collections.Immutable;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.MultiTenancy;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class PermissionDefinition {
        /// <summary>
        /// 权限名称（此值具有唯一性）
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 上级权限
        /// 上级权限授权后才能授权此权限
        /// </summary>
        public PermissionDefinition Parent { get; private set; }

        /// <summary>
        /// 适用的租户类型
        /// 默认值: <see cref="MultiTenancySides.Both"/>
        /// </summary>
        public MultiTenancySides MultiTenancySide { get; set; }

        /// <summary>
        /// 数据权限的范围
        /// </summary>
        public List<PermissionScope> Scopes { get; set; }

        /// <summary>
        /// 权限提供集合
        /// 集合为空，则不限制提供者
        /// </summary>
        public List<string> Providers { get; } //TODO: 考虑重命名为AllowedProviders?

        public ILocalizableString DisplayName {
            get => _displayName;
            set => _displayName = Check.NotNull (value, nameof (value));
        }
        private ILocalizableString _displayName;

        public IReadOnlyList<PermissionDefinition> Children => _children.ToImmutableList ();
        private readonly List<PermissionDefinition> _children;

        /// <summary>
        /// 权限的自定义属性
        /// </summary>
        public Dictionary<string, object> Properties { get; }

        /// <summary>
        /// 定义权限的状态
        /// 通常权限为启用状态
        /// 权限关闭后，任何人将不可见, 但是应用程序仍会检查权限值 (总是返回false)。
        /// 
        /// 默认值: true.
        /// </summary>
        public bool IsEnabled { get; set; }

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

        protected internal PermissionDefinition (
            [NotNull] string name,
            ILocalizableString displayName = null,
            MultiTenancySides multiTenancySide = MultiTenancySides.Both,
            bool isEnabled = true) {
            Name = Check.NotNull (name, nameof (name));
            DisplayName = displayName ?? new FixedLocalizableString (name);
            MultiTenancySide = multiTenancySide;
            IsEnabled = isEnabled;

            Properties = new Dictionary<string, object> ();
            Providers = new List<string> ();
            Scopes = new List<PermissionScope> ();
            _children = new List<PermissionDefinition> ();
        }

        public virtual PermissionDefinition AddChild (
            [NotNull] string name,
            ILocalizableString displayName = null,
            MultiTenancySides multiTenancySide = MultiTenancySides.Both,
            bool isEnabled = true) {
            var child = new PermissionDefinition (
            name,
            displayName,
            multiTenancySide,
            isEnabled) {
            Parent = this
            };

            _children.Add (child);

            return child;
        }

        public virtual PermissionDefinition WithScopes (params PermissionScope[] scopes) {
            if (!scopes.IsNullOrEmpty ()) {
                Scopes.AddRange (scopes);
            }

            return this;
        }

        public virtual PermissionDefinition WithProperty (string key, object value) {
            Properties[key] = value;
            return this;
        }

        public virtual PermissionDefinition WithProviders (params string[] providers) {
            if (!providers.IsNullOrEmpty ()) {
                Providers.AddRange (providers);
            }

            return this;
        }

        public override string ToString () {
            return $"[{nameof(PermissionDefinition)} {Name}]";
        }
    }
}
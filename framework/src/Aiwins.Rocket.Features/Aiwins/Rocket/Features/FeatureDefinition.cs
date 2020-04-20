using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Validation.StringValues;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Features {
    public class FeatureDefinition {
        /// <summary>
        /// 功能名称（具有唯一性）
        /// </summary>
        [NotNull]
        public string Name { get; }

        [NotNull]
        public ILocalizableString DisplayName {
            get => _displayName;
            set => _displayName = Check.NotNull (value, nameof (value));
        }
        private ILocalizableString _displayName;

        [CanBeNull]
        public ILocalizableString Description { get; set; }

        /// <summary>
        /// 上级功能
        /// 上级功能开通后才能开通此功能
        /// </summary>
        [CanBeNull]
        public FeatureDefinition Parent { get; private set; }

        /// <summary>
        /// 下级功能集合
        /// </summary>
        public IReadOnlyList<FeatureDefinition> Children => _children.ToImmutableList ();
        private readonly List<FeatureDefinition> _children;

        /// <summary>
        /// 默认值
        /// </summary>
        [CanBeNull]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 是否对客户端可见
        /// 默认值: true.
        /// </summary>
        public bool IsVisibleToClients { get; set; }

        /// <summary>
        /// 功能提供者集合
        /// 集合为空，则不限制功能的提供者
        /// </summary>
        [NotNull]
        public List<string> AllowedProviders { get; }

        /// <summary>
        /// 自定义属性 <see cref="Properties"/> 索引器
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <returns>
        /// 返回自定义属性字典 <see cref="Properties"/> 中的值
        /// 如果传入的名称 <see cref="name"/> 在自定义属性 <see cref="Properties"/> 字典不存在，则返回null。
        /// </returns>
        [CanBeNull]
        public object this [string name] {
            get => Properties.GetOrDefault (name);
            set => Properties[name] = value;
        }

        /// <summary>
        /// 自定义属性
        /// </summary>
        [NotNull]
        public Dictionary<string, object> Properties { get; }

        /// <summary>
        /// 输入类型
        /// 默认值: <see cref="ToggleStringValueType"/>.
        /// </summary>
        [CanBeNull]
        public IStringValueType ValueType { get; set; }

        public FeatureDefinition (
            string name,
            string defaultValue = null,
            ILocalizableString displayName = null,
            ILocalizableString description = null,
            IStringValueType valueType = null,
            bool isVisibleToClients = true) {
            Name = name;
            DefaultValue = defaultValue;
            DisplayName = displayName ?? new FixedLocalizableString (name);
            Description = description;
            ValueType = valueType;
            IsVisibleToClients = isVisibleToClients;

            Properties = new Dictionary<string, object> ();
            AllowedProviders = new List<string> ();
            _children = new List<FeatureDefinition> ();
        }

        public virtual FeatureDefinition WithProperty (string key, object value) {
            Properties[key] = value;
            return this;
        }

        public virtual FeatureDefinition WithProviders (params string[] providers) {
            if (!providers.IsNullOrEmpty ()) {
                AllowedProviders.AddRange (providers);
            }

            return this;
        }

        /// <summary>
        /// 添加一个子功能
        /// </summary>
        /// <returns>返回新创建的子功能</returns>
        public FeatureDefinition CreateChild (
            string name,
            string defaultValue = null,
            ILocalizableString displayName = null,
            ILocalizableString description = null,
            IStringValueType valueType = null,
            bool isVisibleToClients = true) {
            var feature = new FeatureDefinition (
            name,
            defaultValue,
            displayName,
            description,
            valueType,
            isVisibleToClients) {
            Parent = this
            };

            _children.Add (feature);
            return feature;
        }

        public void RemoveChild (string name) {
            var featureToRemove = _children.FirstOrDefault (f => f.Name == name);
            if (featureToRemove == null) {
                throw new RocketException ($"Could not find a feature named '{name}' in the Children of this feature '{Name}'.");
            }

            featureToRemove.Parent = null;
            _children.Remove (featureToRemove);
        }

        public override string ToString () {
            return $"[{nameof(FeatureDefinition)}: {Name}]";
        }
    }
}
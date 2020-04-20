using System.Collections.Generic;
using System.Collections.Immutable;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Validation.StringValues;

namespace Aiwins.Rocket.Features {
    public class FeatureGroupDefinition {
        /// <summary>
        /// 功能组名称（具有唯一性）
        /// </summary>
        public string Name { get; }

        public Dictionary<string, object> Properties { get; }

        public ILocalizableString DisplayName {
            get => _displayName;
            set => _displayName = Check.NotNull (value, nameof (value));
        }
        private ILocalizableString _displayName;

        public IReadOnlyList<FeatureDefinition> Features => _features.ToImmutableList ();
        private readonly List<FeatureDefinition> _features;

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

        protected internal FeatureGroupDefinition (
            string name,
            ILocalizableString displayName = null) {
            Name = name;
            DisplayName = displayName ?? new FixedLocalizableString (Name);

            Properties = new Dictionary<string, object> ();
            _features = new List<FeatureDefinition> ();
        }

        public virtual FeatureDefinition AddFeature (
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
                isVisibleToClients
            );

            _features.Add (feature);

            return feature;
        }

        public virtual List<FeatureDefinition> GetFeaturesWithChildren () {
            var features = new List<FeatureDefinition> ();

            foreach (var feature in _features) {
                AddFeatureToListRecursively (features, feature);
            }

            return features;
        }

        public virtual FeatureGroupDefinition WithProperty (string key, object value) {
            Properties[key] = value;
            return this;
        }

        private void AddFeatureToListRecursively (List<FeatureDefinition> features, FeatureDefinition feature) {
            features.Add (feature);

            foreach (var child in feature.Children) {
                AddFeatureToListRecursively (features, child);
            }
        }

        public override string ToString () {
            return $"[{nameof(FeatureGroupDefinition)} {Name}]";
        }
    }
}
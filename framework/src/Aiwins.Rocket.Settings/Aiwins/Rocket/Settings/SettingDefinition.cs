using System.Collections.Generic;
using Aiwins.Rocket.Localization;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Settings {
    public class SettingDefinition {
        /// <summary>
        /// 配置名称（唯一性）
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
        /// 默认值
        /// </summary>
        [CanBeNull]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 是否客户端可见
        /// 主要用于控制敏感信息的访问、读取（例如账号、密码等）
        /// 默认值: false.
        /// </summary>
        public bool IsVisibleToClients { get; set; }

        /// <summary>
        /// 允许访问、设置配置信息的提供程序集合
        /// 集合为空表示允许所有的提供程序
        /// </summary>
        public List<string> Providers { get; } //TODO: Rename to AllowedProviders

        /// <summary>
        /// 是否继承父类的设置范围
        /// 默认值: true
        /// </summary>
        public bool IsInherited { get; set; }

        /// <summary>
        /// 参数字典
        /// </summary>
        [NotNull]
        public Dictionary<string, object> Properties { get; }

        /// <summary>
        /// 存储时是否对配置信息进行编码
        /// 默认值: false.
        /// </summary>
        public bool IsEncrypted { get; set; }

        public SettingDefinition (
            string name,
            string defaultValue = null,
            ILocalizableString displayName = null,
            ILocalizableString description = null,
            bool isVisibleToClients = false,
            bool isInherited = true,
            bool isEncrypted = false) {
            Name = name;
            DefaultValue = defaultValue;
            IsVisibleToClients = isVisibleToClients;
            DisplayName = displayName ?? new FixedLocalizableString (name);
            Description = description;
            IsInherited = isInherited;
            IsEncrypted = isEncrypted;

            Properties = new Dictionary<string, object> ();
            Providers = new List<string> ();
        }

        /// <summary>
        /// 设置Properties <see cref="Properties"/> 。
        /// </summary>
        public virtual SettingDefinition WithProperty (string key, object value) {
            Properties[key] = value;
            return this;
        }

        /// <summary>
        /// 设置Providers <see cref="Providers"/>。
        /// </summary>
        public virtual SettingDefinition WithProviders (params string[] providers) {
            if (!providers.IsNullOrEmpty ()) {
                Providers.AddRange (providers);
            }

            return this;
        }
    }
}
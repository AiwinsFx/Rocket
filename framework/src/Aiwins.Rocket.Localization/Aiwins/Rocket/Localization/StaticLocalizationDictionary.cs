using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Aiwins.Rocket.Localization {
    /// <summary>
    ///ILocalizationDictionary <see cref="ILocalizationDictionary"/> 接口。
    /// </summary>
    public class StaticLocalizationDictionary : ILocalizationDictionary {
        /// <inheritdoc/>
        public string CultureName { get; }

        protected Dictionary<string, LocalizedString> Dictionary { get; }

        /// <summary>
        /// 创建一个新的StaticLocalizationDictionary <see cref="StaticLocalizationDictionary"/> 对象.
        /// </summary>
        /// <param name="cultureName">文化主题名称</param>
        /// <param name="dictionary">字典</param>
        public StaticLocalizationDictionary (string cultureName, Dictionary<string, LocalizedString> dictionary) {
            CultureName = cultureName;
            Dictionary = dictionary;
        }

        /// <inheritdoc/>
        public virtual LocalizedString GetOrNull (string name) {
            return Dictionary.GetOrDefault (name);
        }

        public void Fill (Dictionary<string, LocalizedString> dictionary) {
            foreach (var item in Dictionary) {
                dictionary[item.Key] = item.Value;
            }
        }
    }
}
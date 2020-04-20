using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Aiwins.Rocket.Localization {
    /// <summary>
    /// 通过字典存储本地化的信息
    /// </summary>
    public interface ILocalizationDictionary {
        string CultureName { get; }

        LocalizedString GetOrNull (string name);

        void Fill (Dictionary<string, LocalizedString> dictionary);
    }
}
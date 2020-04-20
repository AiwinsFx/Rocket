using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Aiwins.Rocket.Localization {
    public interface ILocalizationResourceContributor {
        void Initialize (LocalizationResourceInitializationContext context);

        LocalizedString GetOrNull (string cultureName, string name);

        void Fill (string cultureName, Dictionary<string, LocalizedString> dictionary);
    }
}
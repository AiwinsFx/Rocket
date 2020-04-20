using System.Collections.Generic;
using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.Localization {
    public class RocketLocalizationOptions {
        public LocalizationResourceDictionary Resources { get; }

        public ITypeList<ILocalizationResourceContributor> GlobalContributors { get; }

        public List<LanguageInfo> Languages { get; }

        public RocketLocalizationOptions () {
            Resources = new LocalizationResourceDictionary ();
            GlobalContributors = new TypeList<ILocalizationResourceContributor> ();
            Languages = new List<LanguageInfo> ();
        }
    }
}
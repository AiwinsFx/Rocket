using System;
using System.Collections.Generic;
using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.Localization {
    public class RocketLocalizationOptions {
        public LocalizationResourceDictionary Resources { get; }

        /// <summary>
        /// Used as the default resource when resource was not specified on a localization operation.
        /// </summary>
        public Type DefaultResourceType { get; set; }

        public ITypeList<ILocalizationResourceContributor> GlobalContributors { get; }

        public List<LanguageInfo> Languages { get; }

        public RocketLocalizationOptions () {
            Resources = new LocalizationResourceDictionary ();
            GlobalContributors = new TypeList<ILocalizationResourceContributor> ();
            Languages = new List<LanguageInfo> ();
        }
    }
}
using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Aiwins.Rocket.Localization {
    public interface IStringLocalizerSupportsInheritance {
        IEnumerable<LocalizedString> GetAllStrings (bool includeParentCultures, bool includeBaseLocalizers);
    }
}
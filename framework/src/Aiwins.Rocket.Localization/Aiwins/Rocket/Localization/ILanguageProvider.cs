using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Localization {
    public interface ILanguageProvider {
        Task<IReadOnlyList<LanguageInfo>> GetLanguagesAsync ();
    }
}
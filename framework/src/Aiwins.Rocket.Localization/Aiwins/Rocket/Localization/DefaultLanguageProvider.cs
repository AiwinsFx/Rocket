using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Localization {
    public class DefaultLanguageProvider : ILanguageProvider, ITransientDependency {
        protected RocketLocalizationOptions Options { get; }

        public DefaultLanguageProvider (IOptions<RocketLocalizationOptions> options) {
            Options = options.Value;
        }

        public Task<IReadOnlyList<LanguageInfo>> GetLanguagesAsync () {
            return Task.FromResult ((IReadOnlyList<LanguageInfo>) Options.Languages);
        }
    }
}
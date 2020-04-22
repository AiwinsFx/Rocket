using System.Collections.Generic;
using Aiwins.Rocket.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.RequestLocalization {
    public class RocketRequestLocalizationOptionsFactory : RocketOptionsFactory<RequestLocalizationOptions> {
        private readonly IRocketRequestLocalizationOptionsProvider _rocketRequestLocalizationOptionsProvider;

        public RocketRequestLocalizationOptionsFactory (
            IRocketRequestLocalizationOptionsProvider rocketRequestLocalizationOptionsProvider,
            IEnumerable<IConfigureOptions<RequestLocalizationOptions>> setups,
            IEnumerable<IPostConfigureOptions<RequestLocalizationOptions>> postConfigures) : base (
            setups,
            postConfigures) {
            _rocketRequestLocalizationOptionsProvider = rocketRequestLocalizationOptionsProvider;
        }

        public override RequestLocalizationOptions Create (string name) {
            return _rocketRequestLocalizationOptionsProvider.GetLocalizationOptions ();
        }
    }
}
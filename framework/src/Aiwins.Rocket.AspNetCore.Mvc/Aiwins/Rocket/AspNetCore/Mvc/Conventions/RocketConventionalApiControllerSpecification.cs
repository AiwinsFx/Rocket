using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Mvc.Conventions {
    public class RocketConventionalApiControllerSpecification : IApiControllerSpecification {
        private readonly RocketAspNetCoreMvcOptions _options;

        public RocketConventionalApiControllerSpecification (IOptions<RocketAspNetCoreMvcOptions> options) {
            _options = options.Value;
        }

        public bool IsSatisfiedBy (ControllerModel controller) {
            var configuration = _options
                .ConventionalControllers
                .ConventionalControllerSettings
                .GetSettingOrNull (controller.ControllerType.AsType ());

            return configuration != null;
        }
    }
}
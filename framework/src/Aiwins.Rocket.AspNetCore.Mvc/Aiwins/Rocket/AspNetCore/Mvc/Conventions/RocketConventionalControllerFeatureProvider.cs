using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Mvc.Conventions {
    public class RocketConventionalControllerFeatureProvider : ControllerFeatureProvider {
        private readonly IRocketApplication _application;

        public RocketConventionalControllerFeatureProvider (IRocketApplication application) {
            _application = application;
        }

        protected override bool IsController (TypeInfo typeInfo) {
            //TODO: Move this to a lazy loaded field for efficiency.
            if (_application.ServiceProvider == null) {
                return false;
            }

            var configuration = _application.ServiceProvider
                .GetRequiredService<IOptions<RocketAspNetCoreMvcOptions>> ().Value
                .ConventionalControllers
                .ConventionalControllerSettings
                .GetSettingOrNull (typeInfo.AsType ());

            return configuration != null;
        }
    }
}
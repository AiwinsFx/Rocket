using Aiwins.Rocket.Application.Services;
using Aiwins.Rocket.Identity.Localization;

namespace Aiwins.Rocket.Identity {
    public abstract class IdentityAppServiceBase : ApplicationService {
        protected IdentityAppServiceBase () {
            ObjectMapperContext = typeof (RocketIdentityApplicationModule);
            LocalizationResource = typeof (IdentityResource);
        }
    }
}
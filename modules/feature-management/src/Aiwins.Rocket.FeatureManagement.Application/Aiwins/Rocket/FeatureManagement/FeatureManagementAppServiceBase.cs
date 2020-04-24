using Aiwins.Rocket.Application.Services;
using Aiwins.Rocket.FeatureManagement.Localization;

namespace Aiwins.Rocket.FeatureManagement
{
    public abstract class FeatureManagementAppServiceBase : ApplicationService
    {
        protected FeatureManagementAppServiceBase()
        {
            ObjectMapperContext = typeof(RocketFeatureManagementApplicationModule);
            LocalizationResource = typeof(RocketFeatureManagementResource);
        }
    }
}
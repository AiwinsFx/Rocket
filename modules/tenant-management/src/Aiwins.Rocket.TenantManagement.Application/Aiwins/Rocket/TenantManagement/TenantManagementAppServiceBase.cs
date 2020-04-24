using Aiwins.Rocket.Application.Services;
using Aiwins.Rocket.TenantManagement.Localization;

namespace Aiwins.Rocket.TenantManagement
{
    public abstract class TenantManagementAppServiceBase : ApplicationService
    {
        protected TenantManagementAppServiceBase()
        {
            ObjectMapperContext = typeof(RocketTenantManagementApplicationModule);
            LocalizationResource = typeof(RocketTenantManagementResource);
        }
    }
}
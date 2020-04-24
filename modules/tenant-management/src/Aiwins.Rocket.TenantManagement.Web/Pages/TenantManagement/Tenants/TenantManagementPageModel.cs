using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;

namespace Aiwins.Rocket.TenantManagement.Web.Pages.TenantManagement.Tenants
{
    public abstract class TenantManagementPageModel : RocketPageModel
    {
        public TenantManagementPageModel()
        {
            ObjectMapperContext = typeof(RocketTenantManagementWebModule);
        }
    }
}
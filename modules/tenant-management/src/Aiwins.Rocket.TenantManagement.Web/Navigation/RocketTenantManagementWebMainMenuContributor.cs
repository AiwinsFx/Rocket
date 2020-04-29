using System.Threading.Tasks;
using Aiwins.Rocket.TenantManagement.Localization;
using Aiwins.Rocket.UI.Navigation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Aiwins.Rocket.TenantManagement.Web.Navigation {
    public class RocketTenantManagementWebMainMenuContributor : IMenuContributor {
        public virtual async Task ConfigureMenuAsync (MenuConfigurationContext context) {
            if (context.Menu.Name != StandardMenus.Main) {
                return;
            }

            var administrationMenu = context.Menu.GetAdministration ();

            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService> ();
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<RocketTenantManagementResource>> ();

            var tenantManagementMenuItem = new ApplicationMenuItem (TenantManagementMenuNames.GroupName, l["Menu:TenantManagement"], icon: "fa fa-users");
            administrationMenu.AddItem (tenantManagementMenuItem);

            if (await authorizationService.IsGrantedAsync (TenantManagementPermissions.Tenants.Default)) {
                tenantManagementMenuItem.AddItem (new ApplicationMenuItem (TenantManagementMenuNames.Tenants, l["Tenants"], url: "/TenantManagement/Tenants"));
            }
        }
    }
}
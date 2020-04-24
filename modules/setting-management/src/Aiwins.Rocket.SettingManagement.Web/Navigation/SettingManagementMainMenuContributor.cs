using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.SettingManagement.Web.Pages.SettingManagement;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.SettingManagement.Localization;

namespace Aiwins.Rocket.SettingManagement.Web.Navigation
{
    public class SettingManagementMainMenuContributor : IMenuContributor
    {
        public virtual async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name != StandardMenus.Main)
            {
                return;
            }

            var settingManagementPageOptions = context.ServiceProvider.GetRequiredService<IOptions<SettingManagementPageOptions>>().Value;
            var settingPageCreationContext = new SettingPageCreationContext(context.ServiceProvider);
            if (
                !settingManagementPageOptions.Contributors.Any() ||
                !(await CheckAnyOfPagePermissionsGranted(settingManagementPageOptions, settingPageCreationContext))
                )
            {
                return;
            }

            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<RocketSettingManagementResource>>();

            context.Menu
                .GetAdministration()
                .AddItem(
                    new ApplicationMenuItem(
                        SettingManagementMenuNames.GroupName,
                        l["Settings"],
                        "/SettingManagement",
                        icon: "fa fa-cog"
                    )
                );
        }

        protected virtual async Task<bool> CheckAnyOfPagePermissionsGranted(
            SettingManagementPageOptions settingManagementPageOptions,
            SettingPageCreationContext settingPageCreationContext)
        {
            foreach (var contributor in settingManagementPageOptions.Contributors)
            {
                if (await contributor.CheckPermissionsAsync(settingPageCreationContext))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

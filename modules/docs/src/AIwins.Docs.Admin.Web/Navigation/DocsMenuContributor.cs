using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Docs.Localization;

namespace Aiwins.Docs.Admin.Navigation
{
    public class DocsMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenu(context);
            }
        }

        private async Task ConfigureMainMenu(MenuConfigurationContext context)
        {

            var administrationMenu = context.Menu.GetAdministration();

            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<DocsResource>>();

            var rootMenuItem = new ApplicationMenuItem(DocsMenuNames.GroupName, l["Menu:DocumentManagement"], icon: "fa fa-book");

            administrationMenu.AddItem(rootMenuItem);

            if (await authorizationService.IsGrantedAsync(DocsAdminPermissions.Projects.Default))
            {
                rootMenuItem.AddItem(new ApplicationMenuItem(DocsMenuNames.Projects, l["Menu:ProjectManagement"], "/Docs/Admin/Projects"));
            }

        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Blogging.Localization;

namespace Aiwins.Blogging
{
    public class BloggingMenuContributor : IMenuContributor
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
            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<BloggingResource>>();

            if (await authorizationService.IsGrantedAsync(BloggingPermissions.Blogs.Management))
            {
                var managementRootMenuItem = new ApplicationMenuItem("BlogManagement", l["Menu:BlogManagement"]);

                //TODO: Using the same permission. Reconsider.
                if (await authorizationService.IsGrantedAsync(BloggingPermissions.Blogs.Management))
                {
                    managementRootMenuItem.AddItem(new ApplicationMenuItem("BlogManagement.Blogs", l["Menu:Blogs"], "/Admin/Blogs"));
                }

                context.Menu.AddItem(managementRootMenuItem);
            }
        }
    }
}

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Account.Web.Modules.Account.Components.Toolbar.UserLoginLink;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Aiwins.Rocket.Users;

namespace Aiwins.Rocket.Account.Web
{
    public class AccountModuleToolbarContributor : IToolbarContributor
    {
        public virtual Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
        {
            if (context.Toolbar.Name != StandardToolbars.Main)
            {
                return Task.CompletedTask;
            }

            //TODO: Currently disabled!
            //if (!context.ServiceProvider.GetRequiredService<ICurrentUser>().IsAuthenticated)
            //{
            //    context.Toolbar.Items.Add(new ToolbarItem(typeof(UserLoginLinkViewComponent)));
            //}

            return Task.CompletedTask;
        }
    }
}

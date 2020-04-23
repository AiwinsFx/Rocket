using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using Aiwins.Rocket.UI.Navigation.Localization.Resource;
using Aiwins.Rocket.UI.Navigation;

namespace Aiwins.Rocket.UI.Navigation
{
    public class DefaultMenuContributor : IMenuContributor
    {
        public virtual Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            Configure(context);
            return Task.CompletedTask;
        }

        protected virtual void Configure(MenuConfigurationContext context)
        {
            var l = context.ServiceProvider
                .GetRequiredService<IStringLocalizer<RocketUiNavigationResource>>();

            if (context.Menu.Name == StandardMenus.Main)
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(
                        DefaultMenuNames.Application.Main.Administration,
                        l["Menu:Administration"],
                        icon: "fa fa-wrench"
                    )
                );
            }
        }
    }
}

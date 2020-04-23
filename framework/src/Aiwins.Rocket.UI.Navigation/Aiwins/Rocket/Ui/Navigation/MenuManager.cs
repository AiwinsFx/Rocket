using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.UI.Navigation
{
    public class MenuManager : IMenuManager, ITransientDependency
    {
        protected RocketNavigationOptions Options { get; }
        protected IHybridServiceScopeFactory ServiceScopeFactory { get; }

        public MenuManager(
            IOptions<RocketNavigationOptions> options, 
            IHybridServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            Options = options.Value;
        }

        public async Task<ApplicationMenu> GetAsync(string name)
        {
            var menu = new ApplicationMenu(name);

            using (var scope = ServiceScopeFactory.CreateScope())
            {
                var context = new MenuConfigurationContext(menu, scope.ServiceProvider);

                foreach (var contributor in Options.MenuContributors)
                {
                    await contributor.ConfigureMenuAsync(context);
                }
            }

            NormalizeMenu(menu);

            return menu;
        }

        protected virtual void NormalizeMenu(IHasMenuItems menuWithItems)
        {
            foreach (var menuItem in menuWithItems.Items)
            {
                NormalizeMenu(menuItem);
            }

            menuWithItems.Items.Normalize();
        }
    }
}
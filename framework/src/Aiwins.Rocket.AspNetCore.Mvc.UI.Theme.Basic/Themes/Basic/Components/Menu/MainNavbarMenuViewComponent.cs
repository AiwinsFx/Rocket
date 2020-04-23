using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.UI.Navigation;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic.Themes.Basic.Components.Menu
{
    public class MainNavbarMenuViewComponent : RocketViewComponent
    {
        private readonly IMenuManager _menuManager;

        public MainNavbarMenuViewComponent(IMenuManager menuManager)
        {
            _menuManager = menuManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = await _menuManager.GetAsync(StandardMenus.Main);
            return View("~/Themes/Basic/Components/Menu/Default.cshtml", menu);
        }
    }
}

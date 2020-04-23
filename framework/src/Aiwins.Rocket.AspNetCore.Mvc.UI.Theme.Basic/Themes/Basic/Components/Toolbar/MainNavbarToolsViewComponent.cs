using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic.Themes.Basic.Components.Toolbar
{
    public class MainNavbarToolbarViewComponent : RocketViewComponent
    {
        private readonly IToolbarManager _toolbarManager;

        public MainNavbarToolbarViewComponent(IToolbarManager toolbarManager)
        {
            _toolbarManager = toolbarManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var toolbar = await _toolbarManager.GetAsync(StandardToolbars.Main);
            return View("~/Themes/Basic/Components/Toolbar/Default.cshtml", toolbar);
        }
    }
}

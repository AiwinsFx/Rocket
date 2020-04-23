using Microsoft.AspNetCore.Mvc;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic.Themes.Basic.Components.MainNavbar
{
    public class MainNavbarViewComponent : RocketViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Themes/Basic/Components/MainNavbar/Default.cshtml");
        }
    }
}

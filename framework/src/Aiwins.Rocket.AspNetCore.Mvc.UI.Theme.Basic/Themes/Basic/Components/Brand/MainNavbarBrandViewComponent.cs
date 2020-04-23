using Microsoft.AspNetCore.Mvc;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic.Themes.Basic.Components.Brand
{
    public class MainNavbarBrandViewComponent : RocketViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Themes/Basic/Components/Brand/Default.cshtml");
        }
    }
}

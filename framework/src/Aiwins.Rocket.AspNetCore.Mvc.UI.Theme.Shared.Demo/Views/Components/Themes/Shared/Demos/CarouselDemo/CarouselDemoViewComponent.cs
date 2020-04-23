using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.CarouselDemo
{
    [Widget]
    public class CarouselDemoViewComponent : RocketViewComponent
    {
        public const string ViewPath = "/Views/Components/Themes/Shared/Demos/CarouselDemo/Default.cshtml";

        public IViewComponentResult Invoke()
        {
            return View(ViewPath);
        }
    }
}

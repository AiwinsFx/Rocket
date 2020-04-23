using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.TooltipsDemo
{
    [Widget]
    public class TooltipsDemoViewComponent : RocketViewComponent
    {
        public const string ViewPath = "/Views/Components/Themes/Shared/Demos/TooltipsDemo/Default.cshtml";

        public IViewComponentResult Invoke()
        {
            return View(ViewPath);
        }
    }
}
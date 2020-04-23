using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.CollapseDemo
{
    [Widget]
    public class CollapseDemoViewComponent : RocketViewComponent
    {
        public const string ViewPath = "/Views/Components/Themes/Shared/Demos/CollapseDemo/Default.cshtml";

        public IViewComponentResult Invoke()
        {
            return View(ViewPath);
        }
    }
}
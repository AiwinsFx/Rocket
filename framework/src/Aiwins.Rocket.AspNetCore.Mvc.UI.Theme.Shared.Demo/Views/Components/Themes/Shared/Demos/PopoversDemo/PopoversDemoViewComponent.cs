using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.PopoversDemo
{
    [Widget]
    public class PopoversDemoViewComponent : RocketViewComponent
    {
        public const string ViewPath = "/Views/Components/Themes/Shared/Demos/PopoversDemo/Default.cshtml";

        public IViewComponentResult Invoke()
        {
            return View(ViewPath);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.DropdownsDemo
{
    [Widget]
    public class DropdownsDemoViewComponent : RocketViewComponent
    {
        public const string ViewPath = "/Views/Components/Themes/Shared/Demos/DropdownsDemo/Default.cshtml";

        public IViewComponentResult Invoke()
        {
            var Model = new DropDownDemoDemoModel();

            return View(ViewPath, Model);
        }
    }
}
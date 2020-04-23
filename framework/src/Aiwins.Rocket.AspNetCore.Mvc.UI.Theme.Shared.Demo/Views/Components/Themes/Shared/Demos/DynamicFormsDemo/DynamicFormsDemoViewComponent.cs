using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.DynamicFormsDemo
{
    [Widget]
    public class DynamicFormsDemoViewComponent : RocketViewComponent
    {
        public const string ViewPath = "/Views/Components/Themes/Shared/Demos/DynamicFormsDemo/Default.cshtml";

        public IViewComponentResult Invoke()
        {
            var model = new DynamicFormsDemoModel();

            return View(ViewPath, model);
        }
    }
}
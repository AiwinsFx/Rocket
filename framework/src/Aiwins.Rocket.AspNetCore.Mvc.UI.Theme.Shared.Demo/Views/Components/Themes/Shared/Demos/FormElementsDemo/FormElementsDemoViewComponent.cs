using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.FormElementsDemo
{
    [Widget]
    public class FormElementsDemoViewComponent : RocketViewComponent
    {
        public const string ViewPath = "/Views/Components/Themes/Shared/Demos/FormElementsDemo/Default.cshtml";

        public IViewComponentResult Invoke()
        {
            var model = new FormElementsDemoModel();

            return View(ViewPath, model);
        }
    }
}

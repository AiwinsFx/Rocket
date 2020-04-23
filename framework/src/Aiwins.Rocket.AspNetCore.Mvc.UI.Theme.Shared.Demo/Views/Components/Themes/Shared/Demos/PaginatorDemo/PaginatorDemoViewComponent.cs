using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.PaginatorDemo
{
    [Widget]
    public class PaginatorDemoViewComponent : RocketViewComponent
    {
        public const string ViewPath = "/Views/Components/Themes/Shared/Demos/PaginatorDemo/Default.cshtml";

        public PagerModel PagerModel { get; set; }

        public IViewComponentResult Invoke(PagerModel pagerModel)
        {
            PagerModel = pagerModel;

            return View(ViewPath);
        }
    }
}
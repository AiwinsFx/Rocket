using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets.Components.WidgetStyles
{
    public class WidgetStylesViewComponent : RocketViewComponent
    {
        protected IPageWidgetManager PageWidgetManager { get; }
        protected RocketWidgetOptions Options { get; }

        public WidgetStylesViewComponent(
            IPageWidgetManager pageWidgetManager,
            IOptions<RocketWidgetOptions> options)
        {
            PageWidgetManager = pageWidgetManager;
            Options = options.Value;
        }

        public virtual IViewComponentResult Invoke()
        {
            return View(
                "~/Aiwins/Rocket/AspNetCore/Mvc/UI/Widgets/Components/WidgetStyles/Default.cshtml",
                new WidgetResourcesViewModel
                {
                    Widgets = PageWidgetManager.GetAll()
                }
            );
        }
    }
}

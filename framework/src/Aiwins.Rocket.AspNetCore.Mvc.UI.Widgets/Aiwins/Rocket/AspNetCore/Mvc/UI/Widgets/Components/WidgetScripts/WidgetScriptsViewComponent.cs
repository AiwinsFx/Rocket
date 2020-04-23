using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets.Components.WidgetStyles;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets.Components.WidgetScripts
{
    public class WidgetScriptsViewComponent : RocketViewComponent
    {
        protected IPageWidgetManager PageWidgetManager { get; }
        protected RocketWidgetOptions Options { get; }

        public WidgetScriptsViewComponent(
            IPageWidgetManager pageWidgetManager,
            IOptions<RocketWidgetOptions> options)
        {
            PageWidgetManager = pageWidgetManager;
            Options = options.Value;
        }

        public virtual IViewComponentResult Invoke()
        {
            return View(
                "~/Aiwins/Rocket/AspNetCore/Mvc/UI/Widgets/Components/WidgetScripts/Default.cshtml",
                new WidgetResourcesViewModel
                {
                    Widgets = PageWidgetManager.GetAll()
                }
            );
        }
    }
}

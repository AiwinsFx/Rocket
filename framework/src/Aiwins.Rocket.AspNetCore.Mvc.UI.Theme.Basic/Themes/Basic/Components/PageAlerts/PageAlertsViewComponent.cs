using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Alerts;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic.Themes.Basic.Components.PageAlerts
{
    public class PageAlertsViewComponent : RocketViewComponent
    {
        private readonly IAlertManager _alertManager;

        public PageAlertsViewComponent(IAlertManager alertManager)
        {
            _alertManager = alertManager;
        }

        public IViewComponentResult Invoke(string name)
        {
            return View("~/Themes/Basic/Components/PageAlerts/Default.cshtml", _alertManager.Alerts);
        }
    }
}

using Aiwins.Rocket.AspNetCore.Mvc.UI.Alerts;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Alert
{
    public class RocketAlertTagHelper : RocketTagHelper<RocketAlertTagHelper, RocketAlertTagHelperService>
    {
        public AlertType AlertType { get; set; } = AlertType.Default;

        public bool? Dismissible { get; set; }

        public RocketAlertTagHelper(RocketAlertTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

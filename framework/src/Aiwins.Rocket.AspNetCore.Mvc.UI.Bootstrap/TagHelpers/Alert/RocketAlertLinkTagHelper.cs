using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Alert
{
    [HtmlTargetElement("a", Attributes = "rocket-alert-link", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class RocketAlertLinkTagHelper : RocketTagHelper<RocketAlertLinkTagHelper, RocketAlertLinkTagHelperService>
    {
        public RocketAlertLinkTagHelper(RocketAlertLinkTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Alert
{
    [HtmlTargetElement("h1", ParentTag = "rocket-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("h2", ParentTag = "rocket-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("h3", ParentTag = "rocket-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("h4", ParentTag = "rocket-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("h5", ParentTag = "rocket-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("h6", ParentTag = "rocket-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class RocketAlertHeaderTagHelper : RocketTagHelper<RocketAlertHeaderTagHelper, RocketAlertHeaderTagHelperService>
    {
        public RocketAlertHeaderTagHelper(RocketAlertHeaderTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

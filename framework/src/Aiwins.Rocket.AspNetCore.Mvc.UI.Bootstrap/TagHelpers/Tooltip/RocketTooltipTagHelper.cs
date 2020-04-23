using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tooltip
{
    [HtmlTargetElement("button", Attributes = "rocket-tooltip")]
    [HtmlTargetElement("button", Attributes = "rocket-tooltip-right")]
    [HtmlTargetElement("button", Attributes = "rocket-tooltip-left")]
    [HtmlTargetElement("button", Attributes = "rocket-tooltip-top")]
    [HtmlTargetElement("button", Attributes = "rocket-tooltip-bottom")]
    [HtmlTargetElement("rocket-button", Attributes = "rocket-tooltip")]
    [HtmlTargetElement("rocket-button", Attributes = "rocket-tooltip-right")]
    [HtmlTargetElement("rocket-button", Attributes = "rocket-tooltip-left")]
    [HtmlTargetElement("rocket-button", Attributes = "rocket-tooltip-top")]
    [HtmlTargetElement("rocket-button", Attributes = "rocket-tooltip-bottom")]
    public class RocketTooltipTagHelper : RocketTagHelper<RocketTooltipTagHelper, RocketTooltipTagHelperService>
    {
        public string RocketTooltip { get; set; }

        public string RocketTooltipRight { get; set; }

        public string RocketTooltipLeft { get; set; }

        public string RocketTooltipTop { get; set; }

        public string RocketTooltipBottom { get; set; }

        public string Title { get; set; }

        public RocketTooltipTagHelper(RocketTooltipTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

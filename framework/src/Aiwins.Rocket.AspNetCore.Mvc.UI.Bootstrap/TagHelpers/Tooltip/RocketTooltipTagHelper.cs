using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tooltip
{
    [HtmlTargetElement("button", Attributes = "abp-tooltip")]
    [HtmlTargetElement("button", Attributes = "abp-tooltip-right")]
    [HtmlTargetElement("button", Attributes = "abp-tooltip-left")]
    [HtmlTargetElement("button", Attributes = "abp-tooltip-top")]
    [HtmlTargetElement("button", Attributes = "abp-tooltip-bottom")]
    [HtmlTargetElement("abp-button", Attributes = "abp-tooltip")]
    [HtmlTargetElement("abp-button", Attributes = "abp-tooltip-right")]
    [HtmlTargetElement("abp-button", Attributes = "abp-tooltip-left")]
    [HtmlTargetElement("abp-button", Attributes = "abp-tooltip-top")]
    [HtmlTargetElement("abp-button", Attributes = "abp-tooltip-bottom")]
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

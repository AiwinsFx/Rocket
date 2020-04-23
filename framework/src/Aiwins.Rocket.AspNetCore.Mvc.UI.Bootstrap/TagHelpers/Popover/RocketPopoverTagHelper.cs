using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Popover
{
    [HtmlTargetElement("button", Attributes = "abp-popover")]
    [HtmlTargetElement("button", Attributes = "abp-popover-right")]
    [HtmlTargetElement("button", Attributes = "abp-popover-left")]
    [HtmlTargetElement("button", Attributes = "abp-popover-top")]
    [HtmlTargetElement("button", Attributes = "abp-popover-bottom")]
    [HtmlTargetElement("abp-button", Attributes = "abp-popover")]
    [HtmlTargetElement("abp-button", Attributes = "abp-popover-right")]
    [HtmlTargetElement("abp-button", Attributes = "abp-popover-left")]
    [HtmlTargetElement("abp-button", Attributes = "abp-popover-top")]
    [HtmlTargetElement("abp-button", Attributes = "abp-popover-bottom")]
    public class RocketPopoverTagHelper : RocketTagHelper<RocketPopoverTagHelper, RocketPopoverTagHelperService>
    {
        public bool? Disabled { get; set; }

        public bool? Dismissible { get; set; }

        public bool? Hoverable { get; set; }

        public string RocketPopover { get; set; }

        public string RocketPopoverRight { get; set; }

        public string RocketPopoverLeft { get; set; }

        public string RocketPopoverTop { get; set; }

        public string RocketPopoverBottom { get; set; }

        public RocketPopoverTagHelper(RocketPopoverTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

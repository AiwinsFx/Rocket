using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Popover
{
    [HtmlTargetElement("button", Attributes = "rocket-popover")]
    [HtmlTargetElement("button", Attributes = "rocket-popover-right")]
    [HtmlTargetElement("button", Attributes = "rocket-popover-left")]
    [HtmlTargetElement("button", Attributes = "rocket-popover-top")]
    [HtmlTargetElement("button", Attributes = "rocket-popover-bottom")]
    [HtmlTargetElement("rocket-button", Attributes = "rocket-popover")]
    [HtmlTargetElement("rocket-button", Attributes = "rocket-popover-right")]
    [HtmlTargetElement("rocket-button", Attributes = "rocket-popover-left")]
    [HtmlTargetElement("rocket-button", Attributes = "rocket-popover-top")]
    [HtmlTargetElement("rocket-button", Attributes = "rocket-popover-bottom")]
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

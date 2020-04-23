using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Border
{
    [HtmlTargetElement(Attributes = "rocket-border")]
    public class RocketBorderTagHelper : RocketTagHelper<RocketBorderTagHelper, RocketBorderTagHelperService>
    {
        public RocketBorderType RocketBorder { get; set; } = RocketBorderType.Default;

        public RocketBorderTagHelper(RocketBorderTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

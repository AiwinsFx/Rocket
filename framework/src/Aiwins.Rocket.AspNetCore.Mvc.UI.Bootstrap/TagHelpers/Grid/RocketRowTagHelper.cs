using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Grid
{
    [HtmlTargetElement("rocket-row")]
    [HtmlTargetElement("rocket-form-row")]
    public class RocketRowTagHelper : RocketTagHelper<RocketRowTagHelper, RocketRowTagHelperService>
    {
        public VerticalAlign VAlign { get; set; } = VerticalAlign.Default;

        public HorizontalAlign HAlign { get; set; } = HorizontalAlign.Default;

        public bool? Gutters { get; set; } = true;

        public RocketRowTagHelper(RocketRowTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

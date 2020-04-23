using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Border
{
    [HtmlTargetElement(Attributes = "abp-rounded")]
    public class RocketRoundedTagHelper : RocketTagHelper<RocketRoundedTagHelper, RocketRoundedTagHelperService>
    {
        public RocketRoundedType RocketRounded { get; set; } = RocketRoundedType.Default;

        public RocketRoundedTagHelper(RocketRoundedTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

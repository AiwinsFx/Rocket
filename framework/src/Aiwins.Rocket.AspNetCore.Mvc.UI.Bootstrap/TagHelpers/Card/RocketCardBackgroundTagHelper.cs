using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    [HtmlTargetElement("abp-card", Attributes = "background")]
    [HtmlTargetElement("abp-card-header", Attributes = "background")]
    [HtmlTargetElement("abp-card-body", Attributes = "background")]
    [HtmlTargetElement("abp-card-footer", Attributes = "background")]
    public class RocketCardBackgroundTagHelper : RocketTagHelper<RocketCardBackgroundTagHelper, RocketCardBackgroundTagHelperService>
    {
        public RocketCardBackgroundType Background { get; set; } = RocketCardBackgroundType.Default;

        public RocketCardBackgroundTagHelper(RocketCardBackgroundTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    [HtmlTargetElement("rocket-card", Attributes = "background")]
    [HtmlTargetElement("rocket-card-header", Attributes = "background")]
    [HtmlTargetElement("rocket-card-body", Attributes = "background")]
    [HtmlTargetElement("rocket-card-footer", Attributes = "background")]
    public class RocketCardBackgroundTagHelper : RocketTagHelper<RocketCardBackgroundTagHelper, RocketCardBackgroundTagHelperService>
    {
        public RocketCardBackgroundType Background { get; set; } = RocketCardBackgroundType.Default;

        public RocketCardBackgroundTagHelper(RocketCardBackgroundTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

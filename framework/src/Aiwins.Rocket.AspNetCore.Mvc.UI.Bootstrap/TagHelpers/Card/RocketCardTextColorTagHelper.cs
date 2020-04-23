using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    [HtmlTargetElement("rocket-card", Attributes = "text-color")]
    [HtmlTargetElement("rocket-card-header", Attributes = "text-color")]
    [HtmlTargetElement("rocket-card-body", Attributes = "text-color")]
    [HtmlTargetElement("rocket-card-footer", Attributes = "text-color")]
    public class RocketCardTextColorTagHelper : RocketTagHelper<RocketCardTextColorTagHelper, RocketCardTextColorTagHelperService>
    {
        public RocketCardTextColorType TextColor { get; set; } = RocketCardTextColorType.Default;

        public RocketCardTextColorTagHelper(RocketCardTextColorTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

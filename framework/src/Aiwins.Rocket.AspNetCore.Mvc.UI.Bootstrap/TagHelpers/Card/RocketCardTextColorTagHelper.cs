using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    [HtmlTargetElement("abp-card", Attributes = "text-color")]
    [HtmlTargetElement("abp-card-header", Attributes = "text-color")]
    [HtmlTargetElement("abp-card-body", Attributes = "text-color")]
    [HtmlTargetElement("abp-card-footer", Attributes = "text-color")]
    public class RocketCardTextColorTagHelper : RocketTagHelper<RocketCardTextColorTagHelper, RocketCardTextColorTagHelperService>
    {
        public RocketCardTextColorType TextColor { get; set; } = RocketCardTextColorType.Default;

        public RocketCardTextColorTagHelper(RocketCardTextColorTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

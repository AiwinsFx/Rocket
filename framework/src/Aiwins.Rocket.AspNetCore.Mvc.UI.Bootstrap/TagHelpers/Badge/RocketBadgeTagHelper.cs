using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Badge
{

    [HtmlTargetElement("a", Attributes = "abp-badge")]
    [HtmlTargetElement("span", Attributes = "abp-badge")]
    [HtmlTargetElement("a", Attributes = "abp-badge-pill")]
    [HtmlTargetElement("span", Attributes = "abp-badge-pill")]
    public class RocketBadgeTagHelper : RocketTagHelper<RocketBadgeTagHelper, RocketBadgeTagHelperService>
    {
        [HtmlAttributeName("abp-badge")]
        public RocketBadgeType BadgeType { get; set; } = RocketBadgeType._;

        [HtmlAttributeName("abp-badge-pill")]
        public RocketBadgeType BadgePillType { get; set; } = RocketBadgeType._;

        public RocketBadgeTagHelper(RocketBadgeTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

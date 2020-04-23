using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Badge
{

    [HtmlTargetElement("a", Attributes = "rocket-badge")]
    [HtmlTargetElement("span", Attributes = "rocket-badge")]
    [HtmlTargetElement("a", Attributes = "rocket-badge-pill")]
    [HtmlTargetElement("span", Attributes = "rocket-badge-pill")]
    public class RocketBadgeTagHelper : RocketTagHelper<RocketBadgeTagHelper, RocketBadgeTagHelperService>
    {
        [HtmlAttributeName("rocket-badge")]
        public RocketBadgeType BadgeType { get; set; } = RocketBadgeType._;

        [HtmlAttributeName("rocket-badge-pill")]
        public RocketBadgeType BadgePillType { get; set; } = RocketBadgeType._;

        public RocketBadgeTagHelper(RocketBadgeTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

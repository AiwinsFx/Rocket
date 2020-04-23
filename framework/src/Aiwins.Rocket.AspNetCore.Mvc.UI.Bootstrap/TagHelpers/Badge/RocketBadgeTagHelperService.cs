using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Badge
{
    public class RocketBadgeTagHelperService : RocketTagHelperService<RocketBadgeTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            SetBadgeClass(context, output);
            SetBadgeStyle(context, output);
        }

        protected virtual void SetBadgeStyle(TagHelperContext context, TagHelperOutput output)
        {
            var badgeType = GetBadgeType(context, output);

            if (badgeType != RocketBadgeType.Default && badgeType != RocketBadgeType._)
            {
                output.Attributes.AddClass("badge-" + badgeType.ToString().ToLowerInvariant());
            }
        }

        protected virtual void SetBadgeClass(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.AddClass("badge");

            if (TagHelper.BadgePillType != RocketBadgeType._)
            {
                output.Attributes.AddClass("badge-pill");
            }
        }

        protected virtual RocketBadgeType GetBadgeType(TagHelperContext context, TagHelperOutput output)
        {
            return TagHelper.BadgeType != RocketBadgeType._ ? TagHelper.BadgeType : TagHelper.BadgePillType;
        }
    }
}
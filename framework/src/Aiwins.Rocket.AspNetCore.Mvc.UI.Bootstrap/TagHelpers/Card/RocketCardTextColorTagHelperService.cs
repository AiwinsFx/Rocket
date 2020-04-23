using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    public class RocketCardTextColorTagHelperService : RocketTagHelperService<RocketCardTextColorTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            SetTextColor(context, output);
        }

        protected virtual void SetTextColor(TagHelperContext context, TagHelperOutput output)
        {
            if (TagHelper.TextColor == RocketCardTextColorType.Default)
            {
                return;
            }

            output.Attributes.AddClass("text-" + TagHelper.TextColor.ToString().ToLowerInvariant());
        }
    }
}
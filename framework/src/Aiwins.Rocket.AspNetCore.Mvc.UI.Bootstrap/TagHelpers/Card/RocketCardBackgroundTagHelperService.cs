using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    public class RocketCardBackgroundTagHelperService : RocketTagHelperService<RocketCardBackgroundTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            SetBackground(context, output);
        }

        protected virtual void SetBackground(TagHelperContext context, TagHelperOutput output)
        {
            if (TagHelper.Background == RocketCardBackgroundType.Default)
            {
                return;
            }

            output.Attributes.AddClass("bg-" + TagHelper.Background.ToString().ToLowerInvariant());
        }
    }
}
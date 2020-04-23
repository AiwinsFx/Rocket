using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    public class RocketCardTagHelperService : RocketTagHelperService<RocketCardTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.AddClass("card");

            SetBorder(context, output);
        }
        protected virtual void SetBorder(TagHelperContext context, TagHelperOutput output)
        {
            if (TagHelper.Border == RocketCardBorderColorType.Default)
            {
                return;
            }

            output.Attributes.AddClass("border-" + TagHelper.Border.ToString().ToLowerInvariant());
        }
    }
}
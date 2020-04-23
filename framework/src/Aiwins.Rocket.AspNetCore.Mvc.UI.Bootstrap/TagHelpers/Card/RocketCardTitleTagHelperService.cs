using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    public class RocketCardTitleTagHelperService : RocketTagHelperService<RocketCardTitleTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = TagHelper.Heading.ToHtmlTag();
            output.Attributes.AddClass("card-title");
        }
    }
}
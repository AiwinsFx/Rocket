using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ListGroup
{
    public class RocketListGroupTagHelperService : RocketTagHelperService<RocketListGroupTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Attributes.AddClass("list-group");

            if (TagHelper.Flush ?? false)
            {
                output.Attributes.AddClass("list-group-flush");
            }
        }
    }
}
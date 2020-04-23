using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    public class RocketCardImageTagHelperService : RocketTagHelperService<RocketCardImageTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.AddClass(TagHelper.Position.ToClassName());
            output.Attributes.RemoveAll("abp-card-image");
        }
    }
}
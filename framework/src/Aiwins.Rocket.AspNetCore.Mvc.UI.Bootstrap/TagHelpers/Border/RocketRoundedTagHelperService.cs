using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Border
{
    public class RocketRoundedTagHelperService : RocketTagHelperService<RocketRoundedTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var roundedClass = "rounded";

            if (TagHelper.RocketRounded != RocketRoundedType.Default)
            {
                roundedClass += "-" + TagHelper.RocketRounded.ToString().ToLowerInvariant().Replace("_","");
            }

            output.Attributes.AddClass(roundedClass);
        }
    }
}
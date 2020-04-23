using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table
{
    public class RocketTableStyleTagHelperService : RocketTagHelperService<RocketTableStyleTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            SetStyle(context,output);
        }

        protected virtual void SetStyle(TagHelperContext context, TagHelperOutput output)
        {
            if (TagHelper.TableStyle != RocketTableStyle.Default)
            {
                output.Attributes.AddClass("table-" + TagHelper.TableStyle.ToString().ToLowerInvariant());
            }
        }
    }
}
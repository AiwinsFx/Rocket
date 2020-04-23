using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table
{
    public class RocketTableHeaderTagHelperService : RocketTagHelperService<RocketTableHeaderTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            SetTheme(context, output);
        }

        protected virtual void SetTheme(TagHelperContext context, TagHelperOutput output)
        {
            switch (TagHelper.Theme)
            {
                case RocketTableHeaderTheme.Default:
                    return;
                case RocketTableHeaderTheme.Dark:
                    output.Attributes.AddClass("thead-dark");
                    return;
                case RocketTableHeaderTheme.Light:
                    output.Attributes.AddClass("thead-light");
                    return;
            }
        }
    }
}
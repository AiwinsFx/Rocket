using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table
{
    [HtmlTargetElement("thead")]
    public class RocketTableHeaderTagHelper : RocketTagHelper<RocketTableHeaderTagHelper, RocketTableHeaderTagHelperService>
    {
        public RocketTableHeaderTheme Theme { get; set; } = RocketTableHeaderTheme.Default;
        
        public RocketTableHeaderTagHelper(RocketTableHeaderTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

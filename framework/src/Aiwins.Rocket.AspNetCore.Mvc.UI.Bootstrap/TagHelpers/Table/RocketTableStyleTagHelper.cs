using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table
{
    [HtmlTargetElement("tr")]
    [HtmlTargetElement("td")]
    public class RocketTableStyleTagHelper : RocketTagHelper<RocketTableStyleTagHelper, RocketTableStyleTagHelperService>
    {
        public RocketTableStyle TableStyle { get; set; } = RocketTableStyle.Default;

        public RocketTableStyleTagHelper(RocketTableStyleTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

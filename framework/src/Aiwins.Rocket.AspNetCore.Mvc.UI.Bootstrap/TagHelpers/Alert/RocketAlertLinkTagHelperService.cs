using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Alert
{
    public class RocketAlertLinkTagHelperService : RocketTagHelperService<RocketAlertLinkTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.AddClass("alert-link");
            output.Attributes.RemoveAll("abp-alert-link");
        }
    }
}
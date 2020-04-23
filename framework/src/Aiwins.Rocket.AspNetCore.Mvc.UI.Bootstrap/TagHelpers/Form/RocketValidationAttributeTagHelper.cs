using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form
{
    [HtmlTargetElement(Attributes = "asp-validation-for")]
    [HtmlTargetElement(Attributes = "asp-validation-summary")]
    public class RocketValidationAttributeTagHelper : RocketTagHelper<RocketValidationAttributeTagHelper, RocketValidationAttributeTagHelperService>, ITransientDependency
    {
        public RocketValidationAttributeTagHelper(RocketValidationAttributeTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

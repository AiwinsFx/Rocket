using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form
{
    [HtmlTargetElement("abp-form-content", TagStructure = TagStructure.WithoutEndTag)]
    public class RocketFormContentTagHelper : RocketTagHelper<RocketFormContentTagHelper, RocketFormContentTagHelperService>, ITransientDependency
    {
        public RocketFormContentTagHelper(RocketFormContentTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

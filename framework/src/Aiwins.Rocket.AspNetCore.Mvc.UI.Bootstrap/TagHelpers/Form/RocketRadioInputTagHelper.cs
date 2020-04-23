using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form
{
    [HtmlTargetElement("rocket-radio")]
    public class RocketRadioInputTagHelper : RocketTagHelper<RocketRadioInputTagHelper, RocketRadioInputTagHelperService>
    {
        public ModelExpression AspFor { get; set; }

        public string Label { get; set; }

        public bool? Inline { get; set; }

        public bool? Disabled { get; set; }

        public IEnumerable<SelectListItem> AspItems { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public RocketRadioInputTagHelper(RocketRadioInputTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

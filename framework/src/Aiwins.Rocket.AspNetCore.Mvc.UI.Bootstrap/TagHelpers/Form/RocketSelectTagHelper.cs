using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form
{
    public class RocketSelectTagHelper : RocketTagHelper<RocketSelectTagHelper, RocketSelectTagHelperService>
    {
        public ModelExpression AspFor { get; set; }

        public string Label { get; set; }

        public IEnumerable<SelectListItem> AspItems { get; set; }

        public RocketFormControlSize Size { get; set; } = RocketFormControlSize.Default;

        [HtmlAttributeName("info")]
        public string InfoText { get; set; }

        [HtmlAttributeName("required-symbol")]
        public bool DisplayRequiredSymbol { get; set; } = true;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public RocketSelectTagHelper(RocketSelectTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

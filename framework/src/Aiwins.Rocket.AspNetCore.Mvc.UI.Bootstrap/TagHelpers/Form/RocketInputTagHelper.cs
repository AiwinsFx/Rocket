using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form
{
    public class RocketInputTagHelper : RocketTagHelper<RocketInputTagHelper, RocketInputTagHelperService>
    {
        public ModelExpression AspFor { get; set; }

        public string Label { get; set; }

        [HtmlAttributeName("info")]
        public string InfoText { get; set; }

        [HtmlAttributeName("disabled")]
        public bool IsDisabled { get; set; } = false;

        [HtmlAttributeName("readonly")]
        public bool? IsReadonly { get; set; } = false;

        public bool AutoFocus { get; set; }

        public RocketFormControlSize Size { get; set; } = RocketFormControlSize.Default;

        [HtmlAttributeName("required-symbol")]
        public bool DisplayRequiredSymbol { get; set; } = true;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public RocketInputTagHelper(RocketInputTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

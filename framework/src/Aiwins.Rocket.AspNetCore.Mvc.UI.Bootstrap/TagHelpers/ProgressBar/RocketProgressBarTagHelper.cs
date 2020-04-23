using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ProgressBar
{
    [HtmlTargetElement("rocket-progress-bar")]
    [HtmlTargetElement("rocket-progress-part")]
    public class RocketProgressBarTagHelper : RocketTagHelper<RocketProgressBarTagHelper, RocketProgressBarTagHelperService>
    {
        public double Value { get; set; }

        public double MinValue { get; set; } = 0;

        public double MaxValue { get; set; } = 100;

        public RocketProgressBarType Type { get; set; } = RocketProgressBarType.Default;

        public bool? Strip { get; set; }

        public bool? Animation { get; set; }

        public RocketProgressBarTagHelper(RocketProgressBarTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

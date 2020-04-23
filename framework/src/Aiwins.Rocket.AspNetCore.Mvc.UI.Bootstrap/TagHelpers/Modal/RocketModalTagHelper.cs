namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal
{
    public class RocketModalTagHelper : RocketTagHelper<RocketModalTagHelper, RocketModalTagHelperService>
    {
        public RocketModalSize Size { get; set; } = RocketModalSize.Default;

        public bool? Centered { get; set; } = false;

        public bool? Static { get; set; } = false;

        public RocketModalTagHelper(RocketModalTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button
{
    public class RocketButtonGroupTagHelper : RocketTagHelper<RocketButtonGroupTagHelper, RocketButtonGroupTagHelperService>
    {
        public RocketButtonGroupDirection Direction { get; set; } = RocketButtonGroupDirection.Horizontal;

        public RocketButtonGroupSize Size { get; set; } = RocketButtonGroupSize.Default;

        public RocketButtonGroupTagHelper(RocketButtonGroupTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

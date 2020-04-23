namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav
{
    public class RocketNavTagHelper : RocketTagHelper<RocketNavTagHelper, RocketNavTagHelperService>
    {
        public RocketNavAlign Align { get; set; } = RocketNavAlign.Default;

        public NavStyle NavStyle { get; set; } = NavStyle.Default;

        public RocketNavTagHelper(RocketNavTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

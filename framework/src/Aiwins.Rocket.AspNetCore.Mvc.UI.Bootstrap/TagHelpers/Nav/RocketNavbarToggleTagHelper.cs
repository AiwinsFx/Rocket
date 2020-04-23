namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav
{
    public class RocketNavbarToggleTagHelper : RocketTagHelper<RocketNavbarToggleTagHelper, RocketNavbarToggleTagHelperService>
    {
        public string Id { get; set; }

        public RocketNavbarToggleTagHelper(RocketNavbarToggleTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

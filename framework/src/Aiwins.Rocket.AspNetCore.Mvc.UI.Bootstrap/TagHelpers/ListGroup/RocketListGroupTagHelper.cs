namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ListGroup
{
    public class RocketListGroupTagHelper : RocketTagHelper<RocketListGroupTagHelper, RocketListGroupTagHelperService>
    {
        public bool? Flush { get; set; }

        public RocketListGroupTagHelper(RocketListGroupTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

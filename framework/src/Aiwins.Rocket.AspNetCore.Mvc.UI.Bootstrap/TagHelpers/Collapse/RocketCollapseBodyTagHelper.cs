namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse
{
    public class RocketCollapseBodyTagHelper : RocketTagHelper<RocketCollapseBodyTagHelper, RocketCollapseBodyTagHelperService>
    {
        public string Id { get; set; }

        public bool? Multi { get; set; }

        public bool? Show { get; set; }

        public RocketCollapseBodyTagHelper(RocketCollapseBodyTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

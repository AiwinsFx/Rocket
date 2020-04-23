namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse
{
    public class RocketAccordionTagHelper : RocketTagHelper<RocketAccordionTagHelper, RocketAccordionTagHelperService>
    {
        public string Id { get; set; }

        public RocketAccordionTagHelper(RocketAccordionTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

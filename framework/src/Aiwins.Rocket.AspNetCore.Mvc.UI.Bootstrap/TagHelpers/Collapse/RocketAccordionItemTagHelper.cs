namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse
{
    public class RocketAccordionItemTagHelper : RocketTagHelper<RocketAccordionItemTagHelper, RocketAccordionItemTagHelperService>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public bool? Active { get; set; }

        public RocketAccordionItemTagHelper(RocketAccordionItemTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

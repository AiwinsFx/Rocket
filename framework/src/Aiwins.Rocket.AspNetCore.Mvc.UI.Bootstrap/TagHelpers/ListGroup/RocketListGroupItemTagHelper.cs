namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ListGroup
{
    public class RocketListGroupItemTagHelper : RocketTagHelper<RocketListGroupItemTagHelper, RocketListGroupItemTagHelperService>
    {
        public bool? Active { get; set; }

        public bool? Disabled { get; set; }

        public string Href { get; set; }

        public RocketListItemTagType TagType { get; set; } = RocketListItemTagType.Default;

        public RocketListItemType Type { get; set; } = RocketListItemType.Default;

        public RocketListGroupItemTagHelper(RocketListGroupItemTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

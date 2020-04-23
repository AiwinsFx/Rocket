namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Breadcrumb
{
    public class RocketBreadcrumbItemTagHelper : RocketTagHelper<RocketBreadcrumbItemTagHelper, RocketBreadcrumbItemTagHelperService>
    {
        public string Href { get; set; }

        public string Title { get; set; }

        public bool Active { get; set; }

        public RocketBreadcrumbItemTagHelper(RocketBreadcrumbItemTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}

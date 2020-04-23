namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    public class RocketStyleTagHelperService : RocketBundleItemTagHelperService<RocketStyleTagHelper>
    {
        public RocketStyleTagHelperService(RocketTagHelperStyleService resourceService)
            : base(resourceService)
        {
        }
    }
}
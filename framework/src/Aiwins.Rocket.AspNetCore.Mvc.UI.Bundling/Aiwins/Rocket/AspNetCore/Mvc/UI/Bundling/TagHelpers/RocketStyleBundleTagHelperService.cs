namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    public class RocketStyleBundleTagHelperService : RocketBundleTagHelperService<RocketStyleBundleTagHelper>
    {
        public RocketStyleBundleTagHelperService(RocketTagHelperStyleService resourceHelper) 
            : base(resourceHelper)
        {
        }
    }
}
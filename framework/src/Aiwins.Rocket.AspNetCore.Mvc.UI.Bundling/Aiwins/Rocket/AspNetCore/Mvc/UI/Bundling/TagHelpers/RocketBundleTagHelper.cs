using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    public abstract class RocketBundleTagHelper<TTagHelper, TService> : RocketTagHelper<TTagHelper, TService>, IBundleTagHelper
        where TTagHelper : RocketTagHelper<TTagHelper, TService>
        where TService : class, IRocketTagHelperService<TTagHelper>
    {
        public string Name { get; set; }

        protected RocketBundleTagHelper(TService service)
            : base(service)
        {

        }

        public virtual string GetNameOrNull()
        {
            return Name;
        }
    }
}
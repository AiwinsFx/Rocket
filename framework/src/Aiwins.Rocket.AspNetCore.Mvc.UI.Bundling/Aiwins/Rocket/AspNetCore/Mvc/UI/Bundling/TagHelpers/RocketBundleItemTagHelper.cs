using System;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    public abstract class RocketBundleItemTagHelper<TTagHelper, TTagHelperService> : RocketTagHelper<TTagHelper, TTagHelperService>, IBundleItemTagHelper 
        where TTagHelper : RocketTagHelper<TTagHelper, TTagHelperService>, IBundleItemTagHelper
        where TTagHelperService: RocketBundleItemTagHelperService<TTagHelper>
    {
        /// <summary>
        /// A file path.
        /// </summary>
        public string Src { get; set; }

        /// <summary>
        /// A bundle contributor type.
        /// </summary>
        public Type Type { get; set; }

        protected RocketBundleItemTagHelper(TTagHelperService service)
            : base(service)
        {
        }

        public string GetNameOrNull()
        {
            if (Type != null)
            {
                return Type.FullName;
            }

            if (Src != null)
            {
                return Src
                    .RemovePreFix("/")
                    .RemovePostFix(StringComparison.OrdinalIgnoreCase, "." + GetFileExtension())
                    .Replace("/", ".");
            }

            throw new RocketException("rocket-script tag helper requires to set either src or type!");
        }

        public BundleTagHelperItem CreateBundleTagHelperItem()
        {
            if (Type != null)
            {
                return new BundleTagHelperContributorTypeItem(Type);
            }

            if (Src != null)
            {
                return new BundleTagHelperFileItem(Src);
            }

            throw new RocketException("rocket-script tag helper requires to set either src or type!");
        }

        protected abstract string GetFileExtension();
    }
}